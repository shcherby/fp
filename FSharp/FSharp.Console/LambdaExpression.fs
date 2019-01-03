module LambdaExpression

// distance
(fun x y -> x - y |> abs) 20 35

// square
List.map (fun x -> x * x) [1;2;3]

// distance
(fun (x:int) (y:int) -> x - y |> abs) 20 35
