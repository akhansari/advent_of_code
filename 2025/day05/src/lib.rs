struct Db {
    fresh: Vec<(usize, usize)>,
    available: Vec<usize>,
}

impl Db {
    fn parse(input: &str) -> Self {
        let (fresh_str, available_str) = input.split_once("\n\n").unwrap();
        Self {
            fresh: fresh_str
                .lines()
                .map(|l| {
                    let (start, end) = l.split_once('-').unwrap();
                    (start.parse().unwrap(), end.parse().unwrap())
                })
                .collect(),
            available: available_str.lines().map(|l| l.parse().unwrap()).collect(),
        }
    }

    fn is_fresh(&self, id: usize) -> bool {
        self.fresh
            .iter()
            .any(|&(start, end)| start <= id && id <= end)
    }
}

pub fn part_one(input: &str) -> usize {
    let db = Db::parse(input);
    db.available.iter().filter(|&&id| db.is_fresh(id)).count()
}

pub fn part_two(input: &str) -> usize {
    let mut merged = Vec::new();
    let mut sorted = Db::parse(input).fresh;
    sorted.sort_by_key(|r| r.0);

    for (start, end) in sorted {
        match merged.last_mut() {
            Some((_, last_end)) if start <= *last_end + 1 => {
                *last_end = end.max(*last_end);
            }
            _ => merged.push((start, end)),
        }
    }

    merged.iter().map(|(start, end)| end - start + 1).sum()
}
