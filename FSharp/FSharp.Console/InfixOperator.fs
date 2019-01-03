module InfixOperator

let (|><|) x y = x - y |> abs

let r = 5 |><| 2 |><| 10