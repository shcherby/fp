namespace FP.TimeTracking.Domain

open System

type [<CLIMutable>] TimeTrack =
  { Id: Guid
    IssueId: Guid
    TimeMinutes: int
    CreatedAt: DateTime
    ReportedDate: DateTime
    CreatedBy: string
    Description: string
  } 