import { readFileSync } from 'fs';
import path, { join } from 'path';


async function run() {
  const content = readFileSync('input').toString('utf-8');
  
  const moves = content.split(/\n/).slice(0, -1).map(l => { return { direction: l.split(' ')[0], distance: l.split(' ')[1]}});

  let rope = [];

  for (let index = 0; index < 10; index++) {
    rope.push({x: 0, y: 0})
  }

  let tailVisited = [];
  moves.forEach(move => {
    
    for (let step = 0; step < move.distance; step++) {
      if(move.direction === 'U') {
        rope[0].y++;
      } else if(move.direction === 'D') {
        rope[0].y--;
      } else if(move.direction === 'R') {
        rope[0].x++;
      } else if(move.direction === 'L') {
        rope[0].x--;
      }

      for (let index = 1; index < rope.length; index++) {
      
        let xDistance = rope[index - 1].x - rope[index].x;
        let yDistance = rope[index - 1].y - rope[index].y;
  
        if((Math.abs(xDistance) >= 1 && Math.abs(yDistance) >= 2) || (Math.abs(xDistance) >= 2 && Math.abs(yDistance) >= 1)) {
          rope[index].x += xDistance > 0 ? 1 : -1;
          rope[index].y += yDistance > 0 ? 1 : -1;
        } else if(Math.abs(xDistance) > 1) {
          rope[index].x += xDistance > 0 ? 1 : -1;
        } else if(Math.abs(yDistance) > 1) {
          rope[index].y += yDistance > 0 ? 1 : -1;
        }
      }

      tailVisited.push({...rope[rope.length - 1]});
    }
  });

  
  const uniqueTailVisits = [...new Set(tailVisited.map(p => `${p.x}__${p.y}`))];

  console.log(uniqueTailVisits.length);
}

run();