namespace FP.TimeTracking.Service

open FP.TimeTracking.Core
open FP.TimeTracking.Domain

module TimeTrackFutureRule =
    let Validate (getDateTime) (timeTrack: TimeTrack) : ValidationResponse =
           let reportDate = timeTrack.ReportedDate.Date
           let currentDate = getDateTime()
           if reportDate > currentDate 
           then (new ValidationResponse([ValidationResult.CreateWarning(sprintf "Time track date (%s) can not be greater than current date (%s)." (reportDate.ToString()) (currentDate.ToString()))]))
           else (new ValidationResponse([]))
