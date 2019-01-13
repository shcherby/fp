namespace FP.TimeTracking.Service

module TimeTracksService = 
    open FP.TimeTracking.Domain
    open FP.TimeTracking.Core

    let Add (timeTrack: TimeTrack) =
        let timeNow = DateTimeProvider.get()
        let timeTrackAdd = 
            { timeTrack with CreatedAt = timeNow }
        let validationResponse = TimeTrackInfoValidator.Validate timeTrackAdd

        if validationResponse.IsValid
        then
            let timeTrackAdded = TimeTrackRepository.Add timeTrackAdd
            (new ValidationResponse<TimeTrack>(timeTrackAdded, [ValidationResult.CreateInfo(sprintf "Time track %s was added." (timeTrackAdded.Id.ToString()))]) :> ValidationResponse)
        else
            (new ValidationResponse(validationResponse.ValidationResults))
