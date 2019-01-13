module PipeOperators

[1;2;3;4]
    |> List.filter (fun i -> i % 2 = 0)
    |> List.map ((*)2)
    |> List.sum

(12,17) ||> min;

min <|| (12,17);