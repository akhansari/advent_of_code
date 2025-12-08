fn distance(a: &[isize], b: &[isize]) -> isize {
    let dx = a[0] - b[0];
    let dy = a[1] - b[1];
    let dz = a[2] - b[2];
    dx * dx + dy * dy + dz * dz
}

struct UnionFind {
    parent: Vec<usize>,
    size: Vec<usize>,
}

// https://www.geeksforgeeks.org/dsa/introduction-to-disjoint-set-data-structure-or-union-find-algorithm
impl UnionFind {
    fn new(n: usize) -> Self {
        UnionFind {
            parent: (0..n).collect(),
            size: vec![1; n],
        }
    }

    fn find(&mut self, x: usize) -> usize {
        if self.parent[x] != x {
            self.parent[x] = self.find(self.parent[x]);
        }
        self.parent[x]
    }

    fn union_by_size(&mut self, x: usize, y: usize) -> bool {
        let root_x = self.find(x);
        let root_y = self.find(y);

        if root_x == root_y {
            return false; // already connected
        }

        if self.size[root_x] < self.size[root_y] {
            self.parent[root_x] = root_y;
            self.size[root_y] += self.size[root_x];
        } else {
            self.parent[root_y] = root_x;
            self.size[root_x] += self.size[root_y];
        }
        true
    }

    fn get_component_sizes(&mut self) -> Vec<usize> {
        let mut sizes = std::collections::HashMap::new();
        for i in 0..self.parent.len() {
            *sizes.entry(self.find(i)).or_insert(0) += 1;
        }
        sizes.into_values().collect()
    }
}

fn parse(input: &str) -> Vec<Vec<isize>> {
    input
        .lines()
        .map(|l| l.split(',').map(|c| c.parse().unwrap()).collect())
        .collect()
}

fn calculate_edges(pos: &[Vec<isize>]) -> Vec<(isize, usize, usize)> {
    let len = pos.len();
    let mut all_edges = Vec::new();
    for i in 0..len {
        for j in (i + 1)..len {
            all_edges.push((distance(&pos[i], &pos[j]), i, j));
        }
    }
    all_edges.sort_unstable();
    all_edges
}

pub fn part_one(input: &str, connections: u16) -> usize {
    let pos = parse(input);
    let all_edges = calculate_edges(&pos);

    let mut uf = UnionFind::new(pos.len());
    let mut attempts = 0;

    for (_, i, j) in all_edges {
        uf.union_by_size(i, j);
        attempts += 1;
        if attempts == connections {
            break;
        }
    }

    let mut sizes = uf.get_component_sizes();
    sizes.sort_unstable_by(|a, b| b.cmp(a)); // descending
    sizes.iter().take(3).product()
}

pub fn part_two(input: &str) -> isize {
    let pos = parse(input);
    let all_edges = calculate_edges(&pos);

    let len = pos.len();
    let mut uf = UnionFind::new(len);
    let mut components = len;

    for (_, i, j) in all_edges {
        if uf.union_by_size(i, j) {
            components -= 1;
            if components == 1 {
                return pos[i][0] * pos[j][0];
            }
        }
    }
    0
}
