use day01::*;

fn main() {
    let input = std::fs::read_to_string("../inputs/day01.txt").expect("Input file not found");
    println!("Part one: {}", part_one(&input));
    println!("Part two: {}", part_two(&input));
}

#[cfg(test)]
mod tests {
    use super::*;

    const INPUT: &str = "L68
L30
R48
L5
R60
L55
L1
L99
R14
L82";

    #[test]
    fn test_part_one() {
        assert_eq!(part_one(INPUT), 3);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(part_two(INPUT), 6);
    }
}
