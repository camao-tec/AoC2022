import { readFileSync } from 'fs';

const sensorsAndBeacons = readFileSync('input').toString('utf-8').split(/\n/).slice(0, -1).map(l => l.split(':').map(p => p.match(/x=(?<x>-?\d+). y=(?<y>-?\d+)/).groups).map(q => {return {x: +q.x, y: +q.y}}));

const allCoordinates = sensorsAndBeacons.reduce((acc, p) => {acc.push(...p); return acc;}, []);

function run() {

  let minX = Math.min(...allCoordinates.map(p => p.x));
  let maxX = Math.max(...allCoordinates.map(p => p.x)) + 1;
  let minY = Math.min(...allCoordinates.map(p => p.y)) - 2;
  let maxY = Math.max(...allCoordinates.map(p => p.y)) + 3;

  const bounds = {x: 4000000, y: 4000000}

  minX = Math.max(minX, 0);
  maxX = Math.min(maxX, bounds.x);
  minY = Math.max(minY, 0);
  maxY = Math.min(maxY, bounds.y);


  const filled = [...new Array(bounds.y + 1)].map(() => []);
  
  sensorsAndBeacons.forEach(([sensor, beacon]) => {
    const distance = Math.abs(sensor.x - beacon.x) + Math.abs(sensor.y - beacon.y);

    for (let y = Math.max(-distance + sensor.y, minY); y <= Math.min(distance + sensor.y, maxY); y++) {
      if((y - sensor.y) <= 0) {
        filled[y].push({
          startX: Math.max(-((y - sensor.y) + distance) + sensor.x, minX),
          endX: Math.min((y - sensor.y) + distance + sensor.x, maxX),
        });
      } else {
        filled[y].push({
          startX: Math.max(-(distance - (y - sensor.y)) + sensor.x, minX),
          endX: Math.min(distance - (y - sensor.y) + sensor.x, maxX),
        });
      }
    }
  });

  const allNew = [];

  for (let y = 0; y < bounds.y + 1; y++) {
    filled[y].sort((a, b) => a.startX - b.startX);
    allNew.push(filled[y][0]);
    for (let index = 1; index < filled[y].length; index++) {
      
      if(allNew[allNew.length - 1].endX >= filled[y][index].startX) {
        allNew[allNew.length - 1].endX = Math.max(allNew[allNew.length - 1].endX, filled[y][index].endX);
      } else {
        console.log((filled[y][index].startX - 1) * 4000000 + y);
        return;
      }
    }
  }
}

run();