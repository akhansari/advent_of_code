fn parse(input: &str) -> Vec<Vec<char>> {
    input.lines().map(|l| l.chars().collect()).collect()
}

fn can_be_removed(x: usize, y: usize, grid: &[Vec<char>]) -> bool {
    grid[y][x] == '@'
        && [
            (-1, -1),
            (0, -1),
            (1, -1),
            (-1, 0),
            (1, 0),
            (-1, 1),
            (0, 1),
            (1, 1),
        ]
        .into_iter()
        .filter_map(|(dx, dy)| {
            let ny = y.checked_add_signed(dy)?;
            let nx = x.checked_add_signed(dx)?;
            grid.get(ny)?.get(nx).filter(|&&c| c == '@')
        })
        .take(4)
        .count()
            < 4
}

pub fn part_one(input: &str) -> usize {
    let grid = parse(input);
    (0..grid.len())
        .map(|y| {
            (0..grid[y].len())
                .filter(|&x| can_be_removed(x, y, &grid))
                .count()
        })
        .sum()
}

pub fn part_two(input: &str) -> usize {
    let mut grid = parse(input);
    std::iter::from_fn(|| {
        let to_remove: Vec<_> = (0..grid.len())
            .flat_map(|y| {
                (0..grid[y].len())
                    .filter_map(|x| can_be_removed(x, y, &grid).then_some((x, y)))
                    .collect::<Vec<_>>()
            })
            .collect();

        (!to_remove.is_empty()).then(|| {
            to_remove.iter().for_each(|(x, y)| grid[*y][*x] = '.');
            to_remove.len()
        })
    })
    .sum()
}
