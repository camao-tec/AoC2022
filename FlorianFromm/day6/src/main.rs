use anyhow::Ok;
use util::read_string;

fn main() -> anyhow::Result<()> {
    let input = read_string(Some("./input.txt".into()))?;
    println!("Part one: {}", solve(&input, 4));
    println!("Part two: {}", solve(&input, 14));
    Ok(())
}

fn solve(input: &String, length: usize) -> usize {
    for index in 0..input.len() - (length - 1) {
        let cond = input[index..index + length].chars().all(|c| {
            input[index..index + length]
                .chars()
                .filter(|cc| c == *cc)
                .count()
                == 1
        });
        if cond {
            return index + length;
        }
    }
    0
}
