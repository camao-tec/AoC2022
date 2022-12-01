import { readFileSync } from 'fs';

async function run() {
  const content = readFileSync('input').toString('utf-8');

  const calories = content.split(/\n\n/).map(p => p.split(/\n/).map(p => +p).reduce((acc, p) => acc + p)).sort((a, b) => a -b);
  const topThree = calories.slice(-3);

  console.log(topThree[0] + topThree[1] + topThree[2]);
}

run();