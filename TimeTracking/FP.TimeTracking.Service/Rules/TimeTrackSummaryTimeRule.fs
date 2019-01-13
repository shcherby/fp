namespace FP.TimeTracking.Service

open FP.TimeTracking.Core
open FP.TimeTracking.Domain

module TimeTrackSummaryTimeRule =

    let Validate (timeTrack: TimeTrack) = 
           let minutesInDay = 60 * 24
           let timeTracksReportedDate = TimeTrackRepository
                                          .GetReportedDate(timeTrack.IssueId,
                                                          timeTrack.CreatedBy,
                                                          timeTrack.ReportedDate)
           let summaryTimeMinutes = timeTracksReportedDate 
                                        |> List.filter (fun t -> not (t.Id = timeTrack.Id))
                                        |> List.sumBy (fun elem -> elem.TimeMinutes)
                                        |> (+) timeTrack.TimeMinutes

           if summaryTimeMinutes > minutesInDay 
           then (new ValidationResponse([ValidationResult.CreateWarning("In sum all time tracks reported for one day can not have more that 24 hours.")]))
           else (new ValidationResponse([]))
