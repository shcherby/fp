namespace FP.TimeTracking.Core

module DateTimeProvider = 
    open System

    let get =  (fun () -> DateTime.Now)

