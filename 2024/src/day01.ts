function parse(input: string) {
  const left = [];
  const right = [];
  for (const line of input.split("\n")) {
    const values = line.split("   ").map(Number);
    left.push(values[0]);
    right.push(values[1]);
  }
  return [left, right];
}

export function runPartOne(input: string): number {
  const [left, right] = parse(input);
  left.sort();
  right.sort();
  return left.reduce((sum, num, i) => sum + Math.abs(num - right[i]), 0);
}

export function runPartTwo(input: string): number {
  const [left, right] = parse(input);
  const counts = right.reduce(
    (map, num) => map.set(num, (map.get(num) || 0) + num),
    new Map(),
  );
  return left.reduce((sum, num) => sum + (counts.get(num) || 0), 0);
}
