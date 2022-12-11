import { readFileSync } from 'fs';


async function run() {
  const content = readFileSync('input').toString('utf-8');
  
  const instructions = content.split(/\n/).slice(0, -1);

  let cycle = 1;
  let x = 1;

  let output = '';

  const doOutput = () => {
    const mod40 = cycle % 40;
    if(x <= mod40 && mod40 < x + 3) {
      output += '#';
    } else {
      output += '.';
    }

    if(mod40 === 0) {
      output += '\n';
    }
  }

  instructions.forEach((i, index, a) => {
    if(i === 'noop') {
      // Nothing

      doOutput();
      cycle++;
    } else {
      const value2Add = +(i.split(' ')[1]);

      doOutput();
      cycle++;


      doOutput();
      cycle++;
      x += value2Add;
    }

  });

  console.log(output);
  // ZRARLFZU
}

run();