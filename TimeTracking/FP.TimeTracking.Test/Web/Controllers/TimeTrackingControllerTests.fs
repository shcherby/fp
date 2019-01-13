namespace FP.TimeTracking.Test.Web.Controllers

open System
open Xunit
open FP.TimeTracking.Domain

module TimeTrackingControllerTests =
    open FP.TimeTracking.Core
    open FP.TimeTracking.Web.ClientModels
    open FP.TimeTracking.Web.Controllers
    open Microsoft.FSharp.Core.Operators

    let getTimeTrackRequestModelMock _ = 
        { IssueId = Guid.Empty;
          CreatedBy = String.Empty
          TimeMinutes = 0
          ReportedDate = DateTime.Now
          Description = String.Empty}

    [<Fact>]
    let ``Post_TimeTrack_Created`` () =
        let sut = new TimeTrackingController()
        let addItemMock (timeTrack: TimeTrack) = (new ValidationResponse([]))
        let deleteItemMock (id: Guid) = (Unchecked.defaultof<TimeTrack>)
        sut.AddDependencies (addItemMock, deleteItemMock)

        let actual = sut.Post(getTimeTrackRequestModelMock())
        
        Assert.Equal(actual.StatusCode, new Nullable<int>(201))

    [<Fact>]
    let ``Post_TimeTrack_BadRequest`` () =
        let sut = new TimeTrackingController()
        let addItemMock (timeTrack: TimeTrack) = (new ValidationResponse([ValidationResult.CreateWarning("")]))
        let deleteItemMock (id: Guid) = (Unchecked.defaultof<TimeTrack>)

        sut.AddDependencies (addItemMock, deleteItemMock)
        let actual = sut.Post(getTimeTrackRequestModelMock())
        
        Assert.Equal(actual.StatusCode, new Nullable<int>(400))

    [<Fact>]
    let ``Delete_TimeTrack`` () =
        let sut = new TimeTrackingController()
        let addItemMock (timeTrack: TimeTrack) = (new ValidationResponse([]))
        let deleteItemMock (id: Guid) = (Unchecked.defaultof<TimeTrack>)
        sut.AddDependencies (addItemMock, deleteItemMock)

        let actual = sut.Delete(Guid.NewGuid())
        
        Assert.Equal(actual.StatusCode, new Nullable<int>(200))
