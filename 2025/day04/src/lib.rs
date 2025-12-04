struct Grid {
    cells: Vec<Vec<char>>,
}

impl Grid {
    fn parse(input: &str) -> Self {
        Self {
            cells: input.lines().map(|l| l.chars().collect()).collect(),
        }
    }

    fn can_be_removed(&self, x: usize, y: usize) -> bool {
        self.cells[y][x] == '@'
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
                self.cells.get(ny)?.get(nx).filter(|&&c| c == '@')
            })
            .take(4)
            .count()
                < 4
    }

    fn find_removable(&self) -> Vec<(usize, usize)> {
        (0..self.cells.len())
            .flat_map(|y| {
                (0..self.cells[y].len())
                    .filter_map(|x| self.can_be_removed(x, y).then_some((x, y)))
                    .collect::<Vec<_>>()
            })
            .collect()
    }
}

pub fn part_one(input: &str) -> usize {
    Grid::parse(input).find_removable().len()
}

pub fn part_two(input: &str) -> usize {
    let mut grid = Grid::parse(input);
    std::iter::from_fn(|| {
        let to_remove = grid.find_removable();
        (!to_remove.is_empty()).then(|| {
            to_remove.iter().for_each(|(x, y)| grid.cells[*y][*x] = '.');
            to_remove.len()
        })
    })
    .sum()
}
