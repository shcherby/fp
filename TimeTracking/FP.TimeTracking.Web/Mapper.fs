module Mapper

open FP.TimeTracking.Web.ClientModels
open FP.TimeTracking.Domain
open System
open FP.TimeTracking.Core

let ToTimeTrack (clientModel: TimeTrackRequestModel) : TimeTrack = 
    { Id = Guid.Empty;
      IssueId = clientModel.IssueId;
      CreatedAt = DateTime.Now
      CreatedBy = clientModel.CreatedBy
      TimeMinutes = clientModel.TimeMinutes
      Description = clientModel.Description
      ReportedDate = clientModel.ReportedDate}

let ToTimeTracks (clientModels: List<TimeTrackRequestModel>) : List<TimeTrack> = 
    clientModels |> List.map ToTimeTrack

let ToMessageResponseModel (vr: ValidationResult) : MessageResponseModel = 
    { Message = vr.Message
      LevelType = vr.Level.ToString()}

let ToResourceResponseModel (vresp: ValidationResponse) : ResourceResponseModelBase = 
     new ResourceResponseModelBase(List.map (fun vr -> ToMessageResponseModel vr) vresp.ValidationResults)

let ToTimeTrackResponseModel (vresp: ValidationResponse<TimeTrack>) : ResourceResponseModelBase = 
     new TimeTrackResponseModel(vresp.Result, List.map (fun vr -> ToMessageResponseModel vr) vresp.ValidationResults) :> ResourceResponseModelBase

let MapResourceResponse (vr: ValidationResponse ): ResourceResponseModelBase = 
    match vr with
        | :? ValidationResponse<TimeTrack> as resp -> (ToTimeTrackResponseModel resp)
        | _ -> (ToResourceResponseModel vr)
        
let ToTimeTrackResponseModels (vresp: List<ValidationResponse>) : List<ResourceResponseModelBase> = 
     List.map (fun vr -> MapResourceResponse(vr)) vresp