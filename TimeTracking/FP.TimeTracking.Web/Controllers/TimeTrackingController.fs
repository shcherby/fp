namespace FP.TimeTracking.Web.Controllers

open System
open Microsoft.AspNetCore.Mvc
open FP.TimeTracking.Web.ClientModels
open FP.TimeTracking.Service
open Mapper
open FP.TimeTracking.Domain

[<Route("api/[controller]")>]
[<ApiController>]
type TimeTrackingController () =
    inherit ControllerBase()
    let mutable addItem = TimeTracksService.Add
    let mutable deleteItem = TimeTrackRepository.Delete

    [<ApiExplorerSettings(IgnoreApi = true)>]
    [<NonAction>]
    member this.AddDependencies (add, delete)=
        addItem <- add
        deleteItem <- delete

    [<HttpGet("{issueId:guid}")>]
    member this.Get(issueId: Guid) =
        let timeTracks = TimeTrackRepository.GetByIssueId issueId
        ActionResult<GetResponseModel>(new GetResponseModel(timeTracks, []))

    [<HttpPost>]
    member this.Post([<FromBody>] timeTrackClientModel: TimeTrackRequestModel) : ObjectResult =
        let response = addItem (ToTimeTrack timeTrackClientModel)
        if response.IsValid 
          then this.Created("Get", MapResourceResponse response) :> ObjectResult
          else this.BadRequest(MapResourceResponse response) :> ObjectResult

    [<HttpDelete("{id}")>]
    member this.Delete(id:Guid) : ObjectResult =
        this.Ok(deleteItem id) :> ObjectResult
