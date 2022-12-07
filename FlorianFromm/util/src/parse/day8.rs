use nom::{
    character::complete::{i32, newline},
    combinator::eof,
    multi::many_till,
    IResult,
};

pub fn parse(source: &str) -> IResult<&str, Vec<()>> {
    let (tail, (values, _)) = many_till(pair_of_number_pairs, eof)(source)?;
    Ok((tail, values))
}

fn pair_of_number_pairs(source: &str) -> IResult<&str, ()> {
    Ok(("", ()))
}
