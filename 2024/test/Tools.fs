[<AutoOpen>]
module Tools

open Xunit

let (=!)<'T> (expected: 'T) (actual: 'T) = Assert.Equal(actual, expected)
