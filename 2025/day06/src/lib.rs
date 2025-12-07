trait Transpose<T> {
    fn transpose(self) -> impl Iterator<Item = Vec<T>>;
}

impl<T: Copy, I> Transpose<T> for I
where
    I: Iterator<Item = Vec<T>>,
{
    fn transpose(self) -> impl Iterator<Item = Vec<T>> {
        let grid: Vec<Vec<T>> = self.collect();
        let (rows, cols) = (grid.len(), grid[0].len());
        (0..cols).map(move |col| (0..rows).map(|row| grid[row][col]).collect())
    }
}

pub fn part_one(input: &str) -> usize {
    input
        .lines()
        .map(|line| line.split_whitespace().collect())
        .transpose()
        .map(|items| -> usize {
            let (&op, num_strs) = items.split_last().unwrap();
            let nums = num_strs.iter().map(|s| s.parse::<usize>().unwrap());
            match op {
                "*" => nums.product(),
                _ => nums.sum(),
            }
        })
        .sum()
}

pub fn part_two(input: &str) -> usize {
    input
        .lines()
        .map(|line| line.chars().rev().collect())
        .transpose()
        .fold((0, Vec::new()), |(sum, mut nums), chars| {
            let (&op, num_chars) = chars.split_last().unwrap();
            let num = num_chars
                .iter()
                .filter(|c| c.is_numeric())
                .fold(0, |n, c| n * 10 + c.to_digit(10).unwrap() as usize);
            if num > 0 {
                nums.push(num);
            }
            match op {
                '*' => (sum + nums.iter().product::<usize>(), Vec::new()),
                '+' => (sum + nums.iter().sum::<usize>(), Vec::new()),
                _ => (sum, nums),
            }
        })
        .0
}
