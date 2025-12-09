use day09::*;

fn main() {
    let input = std::fs::read_to_string("../inputs/day09.txt").expect("Input file not found");
    println!("Part one: {}", part_one(&input));
    let now = std::time::Instant::now();
    println!("Part two: {} in {:.2?}", part_two(&input), now.elapsed());
}

#[cfg(test)]
mod tests {
    use super::*;

    const INPUT: &str = "7,1
11,1
11,7
9,7
9,5
2,5
2,3
7,3";

    #[test]
    fn test_part_one() {
        // between 2,5 and 11,1
        assert_eq!(part_one(INPUT), 50);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(part_two(INPUT), 24);
    }
}
