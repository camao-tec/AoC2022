import { readFileSync } from 'fs';


// A Rock   
// B Paper  
// C Scissor

// X Rock        1P
// Y Paper       2P
// Z Scissor     3P

// Lose 0P
// Win  6p
// Draw 3P

const points = {
  'A X': 0 + 3,
  'A Y': 3 + 1,
  'A Z': 6 + 2,
  'B X': 0 + 1,
  'B Y': 3 + 2,
  'B Z': 6 + 3,
  'C X': 0 + 2,
  'C Y': 3 + 3,
  'C Z': 6 + 1,
}

async function run() {
  const content = readFileSync('input').toString('utf-8');

  let rounds = content.split(/\n/)

  rounds = rounds.slice(0, -1);

  const outcome = rounds.map(round => {
    return points[round];
  });

  console.log(outcome.reduce((acc, p) => acc + p))
}

run();