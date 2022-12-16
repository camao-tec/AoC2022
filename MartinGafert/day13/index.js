import { readFileSync } from 'fs';

function run() {
  let content = readFileSync('input').toString('utf-8');
  content = content.replace(/\n\n/g, '\n');
  const pairs = content.split(/\n/).map(p => JSON.parse(p));

  function compareLists(left, right) {
    for (let index = 0; index < Math.min(left.length, right.length); index++) {
      const leftLeft = left[index];
      const rightRight = right[index];

      if(typeof leftLeft === 'number' && typeof rightRight === 'number') {
        if(leftLeft > rightRight) {
          return false
        } else if(leftLeft < rightRight) {
          return true;
        } else {
          // continue;
        }
      } else if(Array.isArray(leftLeft) && typeof rightRight === 'number') {
        const result = compareLists(leftLeft, [rightRight]);
        if(result !== 'continue') {
          return result;
        }
      } else if(typeof leftLeft === 'number' && Array.isArray(rightRight)) {
        const result = compareLists([leftLeft], rightRight)
        if(result !== 'continue') {
          return result;
        }
      } else if(Array.isArray(leftLeft) && Array.isArray(rightRight)) {
        const result = compareLists(leftLeft, rightRight)
        if(result !== 'continue') {
          return result;
        }
      }
    }

    if(left.length > right.length) {
      return false;
    }
    if(left.length < right.length) {
      return true;
    }

    return 'continue';
  }


  const indexOfRightOrder = [];

  const dividerPackages = [[[2]], [[6]]];
  pairs.push(...dividerPackages);

  pairs.sort((a, b) => {
    const result = compareLists(a, b);

    if(result === 'continue') {
      return 0;
    } else if(result === true) {
      return -1;
    }
    return 1
  });

  const indexes = dividerPackages.map(p => pairs.indexOf(p) + 1);
  
  console.log(indexes[0] * indexes[1]);
}

run();