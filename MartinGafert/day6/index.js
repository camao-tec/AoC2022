import { readFileSync } from 'fs';


async function run() {
  const content = readFileSync('input').toString('utf-8');
  const chars = [...content];

  const byFour = chars.map((c, i, a) => [...a.slice(i, i + 14)]);
  const uniques = byFour.map(b => [...new Set(b)]);
  const index = uniques.findIndex(u => u.length === 14);
  console.log(index + 14);
}

run();