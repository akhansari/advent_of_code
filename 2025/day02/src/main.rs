use day02::*;

fn main() {
    let input = std::fs::read_to_string("../inputs/day02.txt").expect("Input file not found");
    println!("Part one: {}", part_one(&input));
    println!("Part two: {}", part_two(&input));
}

#[cfg(test)]
mod tests {
    use super::*;

    const INPUT: &str = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

    #[test]
    fn test_part_one() {
        assert_eq!(part_one(INPUT), 1227775554);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(part_two(INPUT), 4174379265);
    }
}
