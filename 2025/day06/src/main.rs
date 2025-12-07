use day06::*;

fn main() {
    let input = std::fs::read_to_string("../inputs/day06.txt").expect("Input file not found");
    println!("Part one: {}", part_one(&input));
    let now = std::time::Instant::now();
    println!("Part two: {} in {:.2?}", part_two(&input), now.elapsed());
}

#[cfg(test)]
mod tests {
    use super::*;

    const INPUT: &str = "123 328  51 64 
 45 64  387 23 
  6 98  215 314
*   +   *   +  ";

    #[test]
    fn test_part_one() {
        assert_eq!(part_one(INPUT), 4277556);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(part_two(INPUT), 3263827);
    }
}
