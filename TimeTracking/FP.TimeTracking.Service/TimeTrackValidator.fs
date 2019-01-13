namespace FP.TimeTracking.Service

module TimeTrackInfoValidator =
    open FP.TimeTracking.Domain
    open FP.TimeTracking.Core
    open CollectionExtensions

    let private validate (rules) (timeTrack: TimeTrack) =
        let validations = rules |> List.map (fun (rule) -> rule(timeTrack) :> ValidationResponse)
        let validationResults = selectMany (validations) (fun resp -> seq resp.ValidationResults)
        (new ValidationResponse(Seq.toList validationResults))

    let Validate = validate ([TimeTrackSingleTimeRule.Validate
                              (TimeTrackFutureRule.Validate DateTimeProvider.get);
                              TimeTrackReportedDateRule.Validate;
                              TimeTrackSummaryTimeRule.Validate])
