import { readFileSync } from 'fs';


async function run() {
  const content = readFileSync('input').toString('utf-8');

  let lines = content.split(/\n/);
  lines = lines.slice(0, -1);

  const ranges = lines.map(l => l.split(',').map(m => m.split('-').map(f => +f)))

  const allIncluded = ranges.filter(r => (r[0][0] <= r[1][0] &&  r[1][0] <= r[0][1]) || (r[0][0] <= r[1][1] &&  r[1][1] <= r[0][1]) || (r[1][0] <= r[0][0] &&  r[0][0] <= r[1][1]) || (r[1][0] <= r[0][1] &&  r[0][1] <= r[1][1]));

  console.log(allIncluded.length);
}

run();