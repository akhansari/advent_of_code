import gleam/dict
import gleam/int
import gleam/list
import gleam/option.{None, Some}
import gleam/result
import gleam/string

fn parse(input: String) {
  input
  |> string.split("\n")
  |> list.map(fn(line) {
    line
    |> string.split("   ")
    |> list.map(fn(str) { str |> int.parse |> result.unwrap(0) })
  })
  |> list.transpose
  |> fn(left_right) {
    case left_right {
      [left, right] -> #(left, right)
      _ -> panic as "Bad input"
    }
  }
}

pub fn run_part_one(input) {
  let #(left, right) = parse(input)
  list.map2(
    list.sort(left, int.compare),
    list.sort(right, int.compare),
    fn(l, r) { l - r |> int.absolute_value },
  )
  |> list.fold(0, fn(acc, num) { acc + num })
}

pub fn run_part_two(input) {
  let #(left, right) = parse(input)

  let counts = {
    use acc, num <- list.fold(right, dict.new())
    use old_num <- dict.upsert(acc, num)
    case old_num {
      Some(v) -> v + num
      None -> num
    }
  }

  list.fold(left, 0, fn(acc, num) {
    acc + { counts |> dict.get(num) |> result.unwrap(0) }
  })
}
