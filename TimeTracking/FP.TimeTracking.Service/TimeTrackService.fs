namespace FP.TimeTracking.Service
    open FP.TimeTracking.Domain
    open FP.TimeTracking.Core
    open System

    type ITimeTrackService = 
        abstract GetByIssueId: Guid -> List<TimeTrack>
        abstract Add: TimeTrack -> ValidationResponse
        abstract Delete: Guid -> TimeTrack

    type TimeTrackService() = 
       interface ITimeTrackService with
           member this.GetByIssueId (issueId: Guid) =
                TimeTrackRepository.GetByIssueId issueId
           member this.Add (timeTrack: TimeTrack) =
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
            member this.Delete (id: Guid) =
                TimeTrackRepository.Delete id
