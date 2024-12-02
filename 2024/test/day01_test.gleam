import day01
import gleeunit/should

const input = "3   4
4   3
2   5
1   3
3   9
3   3"

pub fn run_part_one_test() {
  day01.run_part_one(input)
  |> should.equal(11)
}

pub fn run_part_two_test() {
  day01.run_part_two(input)
  |> should.equal(31)
}
