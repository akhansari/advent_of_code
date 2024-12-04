export function runPartOne(input: string) {
  return [...input.matchAll(/mul\((\d+),(\d+)\)/g)].reduce(
    (sum, m) => sum + parseInt(m[1]) * parseInt(m[2]),
    0,
  );
}

export function runPartTwo(input: string): number {
  return Array.from(
    input.matchAll(
      /(?<do>do\(\))|(?<dont>don't\(\))|mul\((?<a>\d+),(?<b>\d+)\)/g,
    ),
  ).reduce<[number, boolean]>(
    ([sum, todo], m) => {
      if (m.groups!.do) return [sum, true];
      else if (m.groups!.dont) return [sum, false];
      else if (todo)
        return [sum + parseInt(m.groups!.a) * parseInt(m.groups!.b), todo];
      return [sum, todo];
    },
    [0, true],
  )[0];
}
