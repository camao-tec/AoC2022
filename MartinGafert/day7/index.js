import { readFileSync } from 'fs';
import path, { join } from 'path';


async function run() {
  const content = readFileSync('input').toString('utf-8');
  
  const commandWithOutputs = content.split(/\$\s*/).slice(1).map(c => c.split(/\n/)).map(c => { return { cmd: c[0].split(' '), output: c.slice(1).filter(l => l.length > 0)}});

  let pwd = '';
  let fileTree = {};
  
  commandWithOutputs.forEach(commandWithOutput => {
    switch ( commandWithOutput.cmd[0]) {
      case 'cd':
        if(commandWithOutput.cmd[1].startsWith('/')) {
          pwd = commandWithOutput.cmd[1];
        } else {
          pwd = join(pwd, commandWithOutput.cmd[1]);
        }
        break;
        
      case 'ls':
        
        fileTree[pwd] = commandWithOutput.output.map(o => o.split(' ')).reduce((acc, v) => {
          if(v[0].match(/\d+/)) {
            acc[v[1]] = +v[0];
          }
          return acc;
        }, {});
        break;
    
      default:
        break;
    }
  });

  const dirSized = {}
  Object.entries(fileTree).forEach(([dir, dirEntry]) => {
    dirSized[dir] = Object.values(dirEntry).reduce((acc, fileSize) => acc + fileSize, 0)
  });

  const dirSummedChilds = {}
  Object.entries(dirSized).forEach(([dir, size], i, a) => {
    
    const subDirSize = a.slice(i + 1).filter(([d, e]) => d.startsWith(dir)).reduce((acc, [_, size]) => acc + size, 0);
    dirSummedChilds[dir] = size + subDirSize;
  });

  const usedSize = dirSummedChilds['/'];
  const freeSize = 70000000 - usedSize;
  const requiredSize = 30000000 - freeSize;


  const bigEnough = Object.entries(dirSummedChilds).filter(([dir, size]) => size >= requiredSize).sort(([_, a], [__, b]) => a - b);
  
  console.log(Object.values(bigEnough)[0][1]);
}

run();