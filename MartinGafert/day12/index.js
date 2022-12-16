import { readFileSync } from 'fs';

const aCode = 'a'.charCodeAt(0)
const startCode = 'S'.charCodeAt(0) - aCode;
const endCode = 'E'.charCodeAt(0) - aCode;

const content = readFileSync('input').toString('utf-8');
const heightMap = content.split(/\n/).slice(0, -1).map(l => [...l].map(c => c.charCodeAt(0) - aCode));
const size = {x: heightMap[0].length, y: heightMap.length };

function findPath(start, end) {
  function next(x, y) {
    const currentHeight = heightMap[y][x];
    const results = [];
    if(x > 0) {
      if(currentHeight + 1 >= heightMap[y][x - 1]) {
        results.push({x: x - 1, y });
      }
    }
    if(y > 0) {
      if(currentHeight + 1 >= heightMap[y - 1][x]) {
        results.push({x, y: y - 1 });
      }
    }
    if(x < size.x - 1) {
      if(currentHeight + 1 >= heightMap[y][x + 1]) {
        results.push({x: x + 1, y });
      }
    }
    if(y < size.y - 1) {
      if(currentHeight + 1 >= heightMap[y + 1][x]) {
        results.push({x, y: y + 1 });
      }
    }

    return results;
  }

  function guess(x, y) {
    const xDistance = Math.abs(x - end.x);
    const yDistance = Math.abs(y - end.y);
    const min = Math.min(xDistance, yDistance);
    const max = Math.max(xDistance, yDistance);

    return min * 1.4 + max - min;
  }




  let costFromSource = {};
  let costEstimate = {};
  let openNodes = [ start ];

  let gotThereFrom = {};

  function rebuildPath() {
    const path = [];
    path.push(end);

    let current = gotThereFrom[`${end.x}_${end.y}`];
    while(current.x !== start.x || current.y !== start.y) {
      path.push(current);
      current = gotThereFrom[`${current.x}_${current.y}`];
    }

    return path;
  }

  function printField() {
    let output = '';
    for (let y = 0; y < size.y; y++) {
      for (let x = 0; x < size.x; x++) {
        if(closedNodes.some(o => o.x === x && o.y === y)) {
          output += "X";
        } else if(openNodes.some(o => o.x === x && o.y === y)) {
          output += "@";
        } else {
          output += String.fromCharCode(heightMap[y][x] + aCode);
        }
      }

      output += '\n';
    }

    console.log(output);
  }

  costFromSource[`${start.x}_${start.y}`] = 0;

  while(openNodes.length > 0) {
    openNodes.sort((a, b) => costEstimate[`${b.x}_${b.y}`] - costEstimate[`${a.x}_${a.y}`]);

    const node = openNodes.pop();

    if(node.x === end.x && node.y === end.y) {
      const path = rebuildPath();
      return path;
    } else {

      const neighbors = next(node.x, node.y);

      neighbors.forEach(neighbor => {
        const costToHere = costFromSource[`${node.x}_${node.y}`] + 1

        if(costToHere < (costFromSource[`${neighbor.x}_${neighbor.y}`] ?? Infinity)) {
          gotThereFrom[`${neighbor.x}_${neighbor.y}`] = node;
          costFromSource[`${neighbor.x}_${neighbor.y}`] = costToHere;
          costEstimate[`${neighbor.x}_${neighbor.y}`] = costToHere + guess(neighbor.x, neighbor.y);

          if(!openNodes.some(i => i.x === neighbor.x && i.y === neighbor.y)) {
            openNodes.push(neighbor);
          }
        }
      });
    }
  }

  return null;
}

async function run() {
  const end = { x: 0, y: 0 };

  for (let y = 0; y < size.y; y++) {
    for (let x = 0; x < size.x; x++) {
      if(heightMap[y][x] === endCode) {
        end.x = x;
        end.y = y;

        heightMap[y][x] = 'z'.charCodeAt(0) - aCode;
      }
    }
  }

  const foundPaths = [];

  for (let y = 0; y < size.y; y++) {
    for (let x = 0; x < size.x; x++) {
      if(heightMap[y][x] === 0) {
        
        const path = findPath({x, y}, end);
        foundPaths.push({x, y, path});
      }
    }
  }

  const withPath = foundPaths.filter(p => p.path !== null);
  
  withPath.sort((a, b) => a.path.length - b.path.length);

  console.log(withPath[0].path.length);
}

run();