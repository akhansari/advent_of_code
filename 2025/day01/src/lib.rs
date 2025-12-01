const DIVISOR: i32 = 100;

fn load_lines(input: &str) -> Vec<String> {
    input.lines().map(str::to_owned).collect()
}

pub fn part_one(input: &str) -> i32 {
    load_lines(input)
        .iter()
        .fold((50, 0), |(clicks, password), line| {
            let num: i32 = line[1..].parse().expect("Invalid number");
            let clicks = match line.chars().next() {
                Some('L') => (clicks - num).rem_euclid(DIVISOR),
                Some('R') => (clicks + num).rem_euclid(DIVISOR),
                _ => panic!("Unexpected character"),
            };
            (clicks, password + (clicks == 0) as i32)
        })
        .1
}

pub fn part_two(input: &str) -> i32 {
    load_lines(input)
        .iter()
        .fold((50, 0), |(clicks, password), line| {
            let num: i32 = line[1..].parse().expect("Invalid number");
            let (clicks, steps_to_zero) = match line.chars().next() {
                Some('L') => (
                    (clicks - num).rem_euclid(DIVISOR),
                    if clicks == 0 { DIVISOR } else { clicks },
                ),
                Some('R') => (
                    (clicks + num).rem_euclid(DIVISOR),
                    if clicks == 0 {
                        DIVISOR
                    } else {
                        DIVISOR - clicks
                    },
                ),
                _ => panic!("Unexpected character"),
            };
            let zeros_crossed = if num >= steps_to_zero {
                1 + (num - steps_to_zero) / DIVISOR
            } else {
                0
            };
            (clicks, password + zeros_crossed)
        })
        .1
}
