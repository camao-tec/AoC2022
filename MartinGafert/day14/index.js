import { readFileSync } from 'fs';


function printMap(map) {
  let output = ''
  for (let y = 0; y < map[0].length; y++) {
    for (let x = 0; x < map.length; x++) {
      output += map[x][y];
    }
    output += '\n';
  }
  process.stdout.write(output);
}

async function wait(ms) {
  return new Promise(res => {
    setTimeout(res, ms);
  });
}

async function run() {
  let content = readFileSync('input').toString('utf-8').split(/\n/).slice(0, -1).map(l => l.split(' -> ').map(p => p.split(',')).map(p => { return { x: +p[0], y: +p[1] };}));

  const allCoordinates = content.reduce((acc, p) => { acc.push(...p); return acc; }, []);
  const minX = Math.min(...allCoordinates.map(p => p.x)) - 100;
  const maxX = Math.max(...allCoordinates.map(p => p.x)) + 300;
  const maxY = Math.max(...allCoordinates.map(p => p.y)) + 2;

  const startPoint = {x: 500 - minX, y: 0}
  const map = [...new Array(maxX - minX)].map((p, x) => [...new Array(maxY)].map((q, y) => x === startPoint.x && y === startPoint.y ? '+' : '.'));

  content.forEach(l => l.forEach((m, i) => {
    if(i === 0) return;
    for (let x = Math.min( l[i - 1].x, m.x); x <=  Math.max( l[i - 1].x, m.x); x++) {
      for (let y = Math.min( l[i - 1].y, m.y); y <=  Math.max( l[i - 1].y, m.y); y++) {
        map[x - minX][y] = '#';
      }
    }
  }));

  let currentSand = {...startPoint};
  let sandDropped = 0;
  while(true) {

    if(map[currentSand.x][currentSand.y + 1] === '.') {
      map[currentSand.x][currentSand.y] = '.';
      currentSand.y++;
      map[currentSand.x][currentSand.y] = 'O';
    } else if(map[currentSand.x - 1][currentSand.y + 1] === '.') {
      map[currentSand.x][currentSand.y] = '.';
      currentSand.x--;
      currentSand.y++;
      map[currentSand.x][currentSand.y] = 'O';
    } else if(map[currentSand.x + 1][currentSand.y + 1] === '.') {
      map[currentSand.x][currentSand.y] = '.';
      currentSand.x++;
      currentSand.y++;
      map[currentSand.x][currentSand.y] = '0';
    } else {
      if(currentSand.x === startPoint.x && currentSand.y === startPoint.y) {
        printMap(map);
        console.log("End");
        console.log(sandDropped + 1);
        break;
      }

      currentSand = {...startPoint};
      sandDropped++;
      map[currentSand.x][currentSand.y] = '0';
    }
  }
}

run();