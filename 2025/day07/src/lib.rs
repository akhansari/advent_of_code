pub fn find_paths(input: &str) -> (usize, usize) {
    let grid: Vec<Vec<char>> = input.lines().map(|l| l.chars().collect()).collect();
    let width = grid[0].len();

    let mut splits = 0;
    let mut paths = vec![0; width];
    paths[grid[0].iter().position(|&c| c == 'S').unwrap()] = 1;

    for row in &grid[1..] {
        let mut next_paths = vec![0; width];
        for (col, &count) in paths.iter().enumerate() {
            if count > 0 {
                if row[col] == '^' {
                    next_paths[col - 1] += count;
                    next_paths[col + 1] += count;
                    splits += 1;
                } else {
                    next_paths[col] += count;
                }
            }
        }
        paths = next_paths;
    }

    (splits, paths.iter().sum())
}

pub fn part_one(input: &str) -> usize {
    find_paths(input).0
}

pub fn part_two(input: &str) -> usize {
    find_paths(input).1
}
