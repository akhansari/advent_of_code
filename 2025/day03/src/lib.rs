fn parse(input: &str) -> Vec<Vec<u64>> {
    input
        .trim()
        .lines()
        .map(|bank| {
            bank.chars()
                .map(|battery| battery.to_digit(10).unwrap() as u64)
                .collect()
        })
        .collect()
}

// Use Monotonic Stack to achieve O(n)
fn largest_subseq(batteries: &[u64], seq_len: usize) -> u64 {
    let mut selected = Vec::new();

    for (i, &bat) in batteries.iter().enumerate() {
        let threshold = seq_len.saturating_sub(batteries.len() - i); // Don't go below 0
        while selected
            .last()
            .is_some_and(|&top| top < bat && selected.len() > threshold)
        {
            selected.pop();
        }
        if selected.len() < seq_len {
            selected.push(bat);
        }
    }

    selected.iter().fold(0_u64, |acc, &d| acc * 10 + d)
}

pub fn part_one(input: &str) -> u64 {
    parse(input)
        .iter()
        .map(|batteries| largest_subseq(batteries, 2))
        .sum()
}

pub fn part_two(input: &str) -> u64 {
    parse(input)
        .iter()
        .map(|batteries| largest_subseq(batteries, 12))
        .sum()
}
