import { readFileSync } from 'fs';


async function run() {
  const content = readFileSync('input').toString('utf-8');

  let lines = content.split(/\n/);
  lines = lines.slice(0, -1);

  const rucksaecke = []
  for (let index = 0; index < lines.length; index+=3) {
    const element = lines[index];
    
    rucksaecke.push({
      first: lines[index],
      second: lines[index + 1],
      third: lines[index + 2]
    });
  };


  const duplicates = rucksaecke.map(r => {
    return [...r.first].find(f => {
      return [...r.second].some(s => {
        return s === f && [...r.third].some(t => t === f);
      });
    })
  });

  const prios = duplicates.map(p => p.charCodeAt(0)).map(p => {
    if (p >= 'A'.charCodeAt(0) && p <= 'Z'.charCodeAt(0)) {
      return p - 'A'.charCodeAt(0) + 27
    } else {
      return p - 'a'.charCodeAt(0) + 1;
    }
  })

  console.log(prios.reduce((acc, p) => acc + p));
}

run();