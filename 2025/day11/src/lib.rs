use std::collections::HashMap;

fn parse(input: &str) -> HashMap<&str, Vec<&str>> {
    input
        .lines()
        .map(|line| {
            let (id, children_str) = line.split_once(":").unwrap();
            let children = children_str.split_whitespace().collect();
            (id, children)
        })
        .collect()
}

fn count_paths<'a>(
    graph: &HashMap<&'a str, Vec<&'a str>>,
    node: &'a str,
    avoid: &[&str],
    memo: &mut HashMap<&'a str, usize>,
) -> usize {
    if node == "out" {
        return 1;
    }
    if avoid.contains(&node) {
        return 0;
    }
    if let Some(&cached) = memo.get(&node) {
        return cached;
    }

    let count = graph
        .get(&node)
        .map(|children| {
            children
                .iter()
                .map(|&child| count_paths(graph, child, avoid, memo))
                .sum()
        })
        .unwrap_or(0);

    memo.insert(node, count);
    count
}

pub fn part_one(input: &str) -> usize {
    count_paths(&parse(input), "you", &[], &mut HashMap::new())
}

pub fn part_two(input: &str) -> usize {
    let graph = parse(input);

    let all = count_paths(&graph, "svr", &[], &mut HashMap::new());
    let no_fft = count_paths(&graph, "svr", &["fft"], &mut HashMap::new());
    let no_dac = count_paths(&graph, "svr", &["dac"], &mut HashMap::new());
    let no_both = count_paths(&graph, "svr", &["fft", "dac"], &mut HashMap::new());

    all - no_fft - no_dac + no_both
}
