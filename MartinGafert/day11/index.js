
function div(check) {
  return (value) => {
    return (value % check === 0);
  }
}

const monkeys = [

  {
    items: [56, 56, 92, 65, 71, 61, 79],
    operation: (old) => old * 7,
    test: div(3),
    trueTarget: 3,
    falseTarget: 7,
    itemsInspected: 0,
  },
  {
    items: [61, 85],
    operation: (old) => old + 5,
    test: div(11),
    trueTarget: 6,
    falseTarget: 4,
    itemsInspected: 0,
  },
  {
    items: [54, 96, 82, 78, 69],
    operation: (old) => old * old,
    test: div(7),
    trueTarget: 0,
    falseTarget: 7,
    itemsInspected: 0,
  },
  {
    items: [57, 59, 65, 95],
    operation: (old) => old + 4,
    test: div(2),
    trueTarget: 5,
    falseTarget: 1,
    itemsInspected: 0,
  },
  {
    items: [62, 67, 80],
    operation: (old) => old * 17,
    test: div(19),
    trueTarget: 2,
    falseTarget: 6,
    itemsInspected: 0,
  },
  {
    items: [91],
    operation: (old) => old + 7,
    test: div(5),
    trueTarget: 1,
    falseTarget: 4,
    itemsInspected: 0,
  },
  {
    items: [79, 83, 64, 52, 77, 56, 63, 92],
    operation: (old) => old + 6,
    test: div(17),
    trueTarget: 2,
    falseTarget: 0,
    itemsInspected: 0,
  },
  {
    items: [50, 97, 76, 96, 80, 56],
    operation: (old) => old + 3,
    test: div(13),
    trueTarget: 3,
    falseTarget: 5,
    itemsInspected: 0,
  }
]

async function run() {
  for (let round = 0; round < 20; round++) {
    
    monkeys.forEach(monkey => {
      monkey.items.forEach(item => {
        item = monkey.operation(item);
        item = Math.floor(item / 3);
        if(monkey.test(item)) {
          monkeys[monkey.trueTarget].items.push(item);
        } else {
          monkeys[monkey.falseTarget].items.push(item);
        }

        monkey.itemsInspected++;
      });
      monkey.items = [];
    });
  }

  const itemsInspected = monkeys.map(m => m.itemsInspected).sort((a, b) => b - a);


  console.log(itemsInspected[0] * itemsInspected[1]);
}

run();