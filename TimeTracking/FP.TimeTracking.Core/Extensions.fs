namespace FP.TimeTracking.Core

module CollectionExtensions = 

    let selectMany (source : 'TSource seq) (selector : 'TSource -> 'TResult seq) =
        source |> Seq.collect selector