import { readFileSync } from 'fs';


async function run() {
  const content = readFileSync('input').toString('utf-8');

  let [stackLines, instructionsLines] = content.split(/\n\n/).map(l => l.split(/\n/));

  stackLines = stackLines.slice(0, -1);
  instructionsLines = instructionsLines.slice(0, -1);

  stackLines.reverse();

  const stacks = stackLines.reduce((acc, s)=> {
    for (let i = 0; i < s.length; i+=4) {
      if(s.substring(i + 1, i + 2) !== ' ') {
        (acc[i / 4] = acc[i / 4] || []).push(s.substring(i + 1, i + 2));
      }
    }
    return acc;
  }, [])

  const instructions = instructionsLines.map(i => {
    const match = i.match(/move (\d+) from (\d+) to (\d+)/)

    return { count: +match[1], from: +match[2] - 1, to: +match[3] - 1};
  });

  instructions.forEach(i => {
    stacks[i.to].push(...stacks[i.from].slice(stacks[i.from].length - i.count));
    stacks[i.from] = stacks[i.from].slice(0, stacks[i.from].length - i.count);
  });

  stacks.map(s => s[s.length - 1]).join('');

  console.log(stacks.map(s => s[s.length - 1]).join(''));
}

run();