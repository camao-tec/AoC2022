import { CrazyMonkey, getInputMonkeyChunks, Monkey, MonkeyOnMyBackpack } from "./anc.ts";

const monkeyChunks = await getInputMonkeyChunks("./input.txt")

const monkeys = monkeyChunks.map((monkeyChunk) => {
    return new Monkey(monkeyChunk)
})

const momb = new MonkeyOnMyBackpack(monkeys)
momb.run()

await momb.manualCheckpoint(20)

console.log(momb.monkeyBusinessLevel())

/// -- END of part one

// part two not solved yet
const crazyMonkeys = monkeyChunks.map((monkeyChunk) => {
    return new CrazyMonkey(monkeyChunk)
})

const cmomb = new MonkeyOnMyBackpack(crazyMonkeys)
cmomb.run()

await cmomb.manualCheckpoint(10000)

console.log(cmomb.monkeyBusinessLevel())

/// -- END of part two
