mod parse;

use anyhow::Result;
use std::fs::read_to_string;

pub fn read_day1(path_to_file: Option<&str>) -> Result<Vec<Vec<i32>>> {
    let file_path = path_to_file.unwrap_or("./input.txt");
    let contents = read_to_string(file_path)?;
    match parse::day1(&contents) {
        Ok((_, values)) => Ok(values),
        Err(err) => anyhow::bail!(err.to_string()),
    }
}

macro_rules! read_day1_tests {
  ($($label:ident: $input:expr, $expected_output:expr,)*) => {
  #[cfg(test)]
  mod tests {
      use super::*;
      $(
          #[test]
          fn $label() {
              match read_day1($input) {
                  Ok(values) => {
                      assert_eq!(values, $expected_output);
                  },
                  Err(err) => panic!("Error: {}", err.to_string())
              }
          }
      )*
  }
  };
}

read_day1_tests! {
  read_day1_file:
    Some("./input_day1.txt"),
    vec![
      vec![1000, 2000, 3000],
      vec![4000],
      vec![5000, 6000],
      vec![7000, 8000, 9000],
      vec![10000],
    ],
}
