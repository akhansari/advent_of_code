use day08::*;

fn main() {
    let input = std::fs::read_to_string("../inputs/day08.txt").expect("Input file not found");
    println!("Part one: {}", part_one(&input, 1000));
    let now = std::time::Instant::now();
    println!("Part two: {} in {:.2?}", part_two(&input), now.elapsed());
}

#[cfg(test)]
mod tests {
    use super::*;

    const INPUT: &str = "162,817,812
57,618,57
906,360,560
592,479,940
352,342,300
466,668,158
542,29,236
431,825,988
739,650,466
52,470,668
216,146,977
819,987,18
117,168,530
805,96,715
346,949,466
970,615,88
941,993,340
862,61,35
984,92,344
425,690,689";

    #[test]
    fn test_part_one() {
        assert_eq!(part_one(INPUT, 10), 40);
    }

    #[test]
    fn test_part_two() {
        assert_eq!(part_two(INPUT), 25272);
    }
}
