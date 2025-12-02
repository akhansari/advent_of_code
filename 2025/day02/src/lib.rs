fn load(input: &str) -> Vec<(u64, u64)> {
    input
        .trim()
        .split(',')
        .map(|range| {
            let mut parts = range.split('-');
            let start: u64 = parts.next().unwrap().parse().unwrap();
            let end: u64 = parts.next().unwrap().parse().unwrap();
            (start, end)
        })
        .collect()
}

fn is_repeated_twice(num: &u64) -> bool {
    // let str = num.to_string();
    // let (left, right) = str.split_at(str.len() / 2);
    // left == right
    // 👇 less allocation approach
    let digits = num.ilog10() + 1;
    if !digits.is_multiple_of(2) {
        return false;
    }
    let half = digits / 2;
    let divisor = 10_u64.pow(half);
    num / divisor == num % divisor
}

fn is_repeated_seq(num: &u64) -> bool {
    let digits = num.ilog10() + 1;
    for seq_len in 1..=(digits / 2) {
        let divisor = 10_u64.pow(seq_len);
        let pattern = num % divisor;
        let repeated = (0..(digits / seq_len)).fold(0, |acc, _| acc * divisor + pattern);
        if repeated == *num {
            return true;
        }
    }
    false
}

fn sum<F>(input: &str, predicate: F) -> u64
where
    F: Fn(&u64) -> bool,
{
    load(input)
        .iter()
        .flat_map(|(start, end)| (*start..=*end).filter(&predicate))
        .sum()
}

pub fn part_one(input: &str) -> u64 {
    sum(input, is_repeated_twice)
}

pub fn part_two(input: &str) -> u64 {
    sum(input, is_repeated_seq)
}
