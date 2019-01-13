namespace FP.TimeTracking.Web.ClientModels

open System
open FP.TimeTracking.Domain

type [<CLIMutable>] TimeTrackRequestModel =
 { IssueId: Guid
   CreatedBy: string
   TimeMinutes: int
   ReportedDate: DateTime
   Description: string
  }

type [<CLIMutable>] MessageResponseModel = 
    {Message: string
     LevelType: string }

type ResourceResponseModelBase(messages) =
   member this.Messages: List<MessageResponseModel> = messages

type TimeTrackResponseModel(data, messages) =
   inherit ResourceResponseModelBase(messages)
   member this.Data: TimeTrack = data

type GetResponseModel(data, messages) =
   inherit ResourceResponseModelBase(messages)
   member this.Data: List<TimeTrack> = data
