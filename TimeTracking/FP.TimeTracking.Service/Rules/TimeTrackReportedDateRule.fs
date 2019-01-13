namespace FP.TimeTracking.Service

open FP.TimeTracking.Core
open System

module TimeTrackReportedDateRule =
    open FP.TimeTracking.Domain

    let Validate (timeTrack: TimeTrack) = 
        let minimumMonth = timeTrack.CreatedAt.AddMonths(-2)
        let minimumValidReportedDate = new DateTime(minimumMonth.Year, minimumMonth.Month, 1)
        if timeTrack.ReportedDate < minimumValidReportedDate
        then (new ValidationResponse([ValidationResult.CreateWarning(sprintf "\"Reported Date\" date is prior to the accessible period, accessible period starts at %s." (minimumValidReportedDate.ToString()))]))
        else (new ValidationResponse([]))
