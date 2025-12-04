use day04::*;

fn main() {
    let input = std::fs::read_to_string("../inputs/day04.txt").expect("Input file not found");
    println!("Part one: {}", part_one(&input));
    println!("Part two: {}", part_two(&input));
}

#[cfg(test)]
mod tests {
    use super::*;

    const INPUT: &str = "..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.";

    #[test]
    fn test_part_one() {
        assert_eq!(part_one(INPUT), 13);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(part_two(INPUT), 43);
    }
}
