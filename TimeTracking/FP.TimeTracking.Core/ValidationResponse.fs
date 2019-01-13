namespace FP.TimeTracking.Core

type LevelType = Info=0 | Warning=1 | Error=2

type ValidationResult (message: string, level: LevelType) =

     member this.Message = message
     member this.Level = level
     member this.IsValid =
        not (level=LevelType.Error) && not (level=LevelType.Warning)

     static member CreateInfo message = new ValidationResult(message, LevelType.Info)
     static member CreateWarning message = new ValidationResult(message, LevelType.Warning)
     static member CreateError message = new ValidationResult(message, LevelType.Error)

type ValidationResponse(validationResults: List<ValidationResult>) =
    member this.ValidationResults = validationResults
    member this.IsValid = List.forall (fun (elem: ValidationResult) -> elem.IsValid) validationResults

type ValidationResponse<'Result>(result: 'Result, validationResults: List<ValidationResult>) =
    inherit ValidationResponse(validationResults)
    member this.Result = result