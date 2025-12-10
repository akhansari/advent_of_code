use day10::*;

fn main() {
    let input = std::fs::read_to_string("../inputs/day10.txt").expect("Input file not found");
    println!("Part one: {}", part_one(&input));
    let now = std::time::Instant::now();
    println!("Part two: {} in {:.2?}", part_two(&input), now.elapsed());
}

#[cfg(test)]
mod tests {
    use super::*;

    const INPUT: &str = "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}
[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}";

    #[test]
    fn test_part_one() {
        assert_eq!(part_one(INPUT), 7);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(part_two(INPUT), 33);
    }
}
