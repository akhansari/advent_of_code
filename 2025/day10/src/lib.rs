#[derive(Debug)]
struct Machine {
    lights: Vec<bool>,
    buttons: Vec<Vec<usize>>,
    joltage: Vec<usize>,
}

fn parse(input: &str) -> Vec<Machine> {
    let nums_re = regex::Regex::new(r"\d+").unwrap();
    let get_nums = |str: &str| -> Vec<usize> {
        nums_re
            .find_iter(str)
            .map(|m| m.as_str().parse().unwrap())
            .collect()
    };
    input
        .lines()
        .map(|l| {
            let parts: Vec<&str> = l.split_whitespace().collect();
            Machine {
                lights: parts[0][1..parts[0].len() - 1]
                    .chars()
                    .map(|c| c == '#')
                    .collect(),
                buttons: parts[1..parts.len() - 1]
                    .iter()
                    .map(|s| get_nums(s))
                    .collect(),
                joltage: get_nums(parts.last().unwrap()),
            }
        })
        .collect()
}

fn fewest_presses(machine: &Machine) -> usize {
    (0..(1 << machine.buttons.len()))
        .filter_map(|mask| {
            let mut state = vec![false; machine.lights.len()];
            let mut presses = 0;

            for (i, button) in machine.buttons.iter().enumerate() {
                if mask & (1 << i) != 0 {
                    presses += 1;
                    for &light in button {
                        state[light] = !state[light];
                    }
                }
            }

            (state == machine.lights).then_some(presses)
        })
        .min()
        .unwrap_or(usize::MAX)
}

pub fn part_one(input: &str) -> usize {
    let machines = parse(input);
    machines.iter().map(fewest_presses).sum()
}

fn fewest_presses_joltage(machine: &Machine) -> usize {
    use microlp::{ComparisonOp, OptimizationDirection, Problem};

    let mut problem = Problem::new(OptimizationDirection::Minimize);

    let vars: Vec<_> = (0..machine.buttons.len())
        .map(|_| problem.add_var(1.0, (0.0, f64::INFINITY)))
        .collect();

    for (counter, &target) in machine.joltage.iter().enumerate() {
        let constraint: Vec<_> = machine
            .buttons
            .iter()
            .enumerate()
            .filter(|(_, btn)| btn.contains(&counter))
            .map(|(i, _)| (vars[i], 1.0))
            .collect();
        problem.add_constraint(&constraint, ComparisonOp::Eq, target as f64);
    }

    problem
        .solve()
        .map(|sol| vars.iter().map(|&v| sol[v].round() as usize).sum())
        .unwrap_or(usize::MAX)
}

pub fn part_two(input: &str) -> usize {
    let machines = parse(input);
    machines.iter().map(fewest_presses_joltage).sum()
}
