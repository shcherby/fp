namespace FP.TimeTracking.Service

open FP.TimeTracking.Core

module TimeTrackSingleTimeRule =
    open FP.TimeTracking.Domain

    let private minutesInDay = 60 * 24

    let Validate (timeTrack: TimeTrack) = 
           if timeTrack.TimeMinutes > minutesInDay 
           then (new ValidationResponse([ValidationResult.CreateWarning("Single time track can not have more that 24 hours.")]))
           else (new ValidationResponse([]))
