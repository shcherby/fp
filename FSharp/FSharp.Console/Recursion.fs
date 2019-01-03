module Recursion

let rec length = function
    | [] -> 0
    | x::xs -> 1 + length xs

let r = length [1;2;3;4];

let rec factorial = function
    | 1 -> 1
    | n -> n * factorial (n-1);

let r1 = factorial 4;