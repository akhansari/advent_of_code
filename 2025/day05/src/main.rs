use day05::*;

fn main() {
    let input = std::fs::read_to_string("../inputs/day05.txt").expect("Input file not found");
    println!("Part one: {}", part_one(&input));
    let now = std::time::Instant::now();
    println!("Part two: {} in {:.2?}", part_two(&input), now.elapsed());
}

#[cfg(test)]
mod tests {
    use super::*;

    const INPUT: &str = "3-5
10-14
16-20
12-18

1
5
8
11
17
32";

    #[test]
    fn test_part_one() {
        assert_eq!(part_one(INPUT), 3);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(part_two(INPUT), 14);
    }
}
