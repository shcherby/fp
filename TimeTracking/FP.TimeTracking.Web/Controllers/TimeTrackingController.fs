namespace FP.TimeTracking.Web.Controllers

open System
open Microsoft.AspNetCore.Mvc
open FP.TimeTracking.Web.ClientModels
open FP.TimeTracking.Service
open Mapper
open FP.TimeTracking.Domain

[<Route("api/[controller]")>]
[<ApiController>]
type TimeTrackingController (timeTrackService: ITimeTrackService) =
    inherit ControllerBase()

    [<HttpGet("{issueId:guid}")>]
    member this.Get(issueId: Guid) =
        let timeTracks = timeTrackService.GetByIssueId issueId
        ActionResult<GetResponseModel>(new GetResponseModel(timeTracks, []))

    [<HttpPost>]
    member this.Post([<FromBody>] timeTrackClientModel: TimeTrackRequestModel) : ObjectResult =
        let response = timeTrackService.Add (ToTimeTrack timeTrackClientModel)
        if response.IsValid 
          then this.Created("Get", MapResourceResponse response) :> ObjectResult
          else this.BadRequest(MapResourceResponse response) :> ObjectResult

    [<HttpDelete("{id}")>]
    member this.Delete(id:Guid) : ObjectResult =
        this.Ok(timeTrackService.Delete id) :> ObjectResult
