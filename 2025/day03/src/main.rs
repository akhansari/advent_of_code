use day03::*;

fn main() {
    let input = std::fs::read_to_string("../inputs/day03.txt").expect("Input file not found");
    println!("Part one: {}", part_one(&input));
    let now = std::time::Instant::now();
    println!("Part two: {} in {:?}", part_two(&input), now.elapsed());
}

#[cfg(test)]
mod tests {
    use super::*;

    const INPUT: &str = "987654321111111
811111111111119
234234234234278
818181911112111";

    #[test]
    fn test_part_one() {
        assert_eq!(part_one(INPUT), 357);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(part_two(INPUT), 3121910778619);
    }
}
