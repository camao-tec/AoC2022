import { readFileSync } from 'fs';
import path, { join } from 'path';


async function run() {
  const content = readFileSync('input').toString('utf-8');
  
  const trees = content.split(/\n/).slice(0, -1).map(l => [...l].map(c => +c));

  let largest = -1;
  for (let row = 0; row < trees.length; row++) {
    for (let col = 0; col < trees[row].length; col++) {
      const tree = trees[row][col];

      let topDownVisible = 0;
      for(let topDown = row - 1; topDown >= 0; topDown--) {
        if(trees[topDown][col] >= tree) {
          topDownVisible++;
          break;
        }
        topDownVisible++;
      }
      

      let bottomUpVisible = 0;
      for (let bottomUp = row + 1; bottomUp < trees.length; bottomUp++) {
        if(trees[bottomUp][col] >= tree) {
          bottomUpVisible++;
          break;
        }
        bottomUpVisible++;
      }

      let leftRightVisible = 0;
      for(let leftRight = col - 1; leftRight >= 0; leftRight--) {
        if(trees[row][leftRight] >= tree) {
          leftRightVisible++;
          break;
        }
        leftRightVisible++;
      }

      let rightLeftVisible = 0;
      for (let rightLeft = col + 1; rightLeft < trees[row].length; rightLeft++) {
        if(trees[row][rightLeft] >= tree) {
          rightLeftVisible++;
          break;
        }
        rightLeftVisible++;
      }
      
      largest = Math.max(largest, topDownVisible * bottomUpVisible * leftRightVisible * rightLeftVisible);
    }
  }

  console.log(largest);
}

run();