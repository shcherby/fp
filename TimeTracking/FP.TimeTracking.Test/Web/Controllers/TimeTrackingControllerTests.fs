namespace FP.TimeTracking.Test.Web.Controllers

open System
open Xunit
open FP.TimeTracking.Domain

module TimeTrackingControllerTests =
    open FP.TimeTracking.Core
    open FP.TimeTracking.Service
    open FP.TimeTracking.Web.ClientModels
    open FP.TimeTracking.Web.Controllers
    open Microsoft.FSharp.Core.Operators

    type TimeTrackServiceMock() = 
        let mutable addItem = new ValidationResponse([])
        let mutable deleteItem = Unchecked.defaultof<TimeTrack>
        member this.SetAdd (validationResponse:ValidationResponse): TimeTrackServiceMock = 
            addItem <- validationResponse
            (this)
        interface ITimeTrackService with
            member this.GetByIssueId (issueId: Guid) = []
            member this.Add (timeTrack: TimeTrack) = addItem
            member this.Delete (id: Guid) = deleteItem

    let getTimeTrackingController _ = 
        new TimeTrackingController(new TimeTrackServiceMock())

    let getTimeTrackRequestModelMock _ = 
        { IssueId = Guid.Empty;
          CreatedBy = String.Empty
          TimeMinutes = 0
          ReportedDate = DateTime.Now
          Description = String.Empty}

    [<Fact>]
    let ``Post_TimeTrack_Created`` () =
        let sut = getTimeTrackingController()
        let actual = sut.Post(getTimeTrackRequestModelMock())
        
        Assert.Equal(actual.StatusCode, new Nullable<int>(201))

    [<Fact>]
    let ``Post_TimeTrack_BadRequest`` () =
        let trackServiceMock = new TimeTrackServiceMock()
        trackServiceMock.SetAdd(new ValidationResponse([ValidationResult.CreateWarning("")])) |> ignore
        let sut = new TimeTrackingController(trackServiceMock)
        let actual = sut.Post(getTimeTrackRequestModelMock())
        
        Assert.Equal(actual.StatusCode, new Nullable<int>(400))

    [<Fact>]
    let ``Delete_TimeTrack`` () =
        let sut = getTimeTrackingController()

        let actual = sut.Delete(Guid.NewGuid())
        
        Assert.Equal(actual.StatusCode, new Nullable<int>(200))
