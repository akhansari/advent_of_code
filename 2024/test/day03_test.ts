import { assertEquals } from "jsr:@std/assert";
import { runPartOne, runPartTwo } from "../src/day03.ts";

Deno.test("test part 1", () => {
  const input =
    "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
  assertEquals(runPartOne(input), 161);
});

Deno.test("test part 2", () => {
  const input =
    "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
  assertEquals(runPartTwo(input), 48);
});
