import { assertEquals } from "jsr:@std/assert";
import { runPartOne, runPartTwo } from "../src/day01.ts";

const input = `3   4
4   3
2   5
1   3
3   9
3   3`;

Deno.test("test day 01 part 1", () => {
  assertEquals(runPartOne(input), 11);
});

Deno.test("test day 01 part 2", () => {
  assertEquals(runPartTwo(input), 31);
});
