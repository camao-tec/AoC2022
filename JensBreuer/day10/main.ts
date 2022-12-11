import { CRT, getInputLines, sum } from "./anc.ts";

const cmds = await getInputLines("./input.txt")

const crt = new CRT(cmds)

crt.run()

const finishPartTwo = crt.manualCheckpoint(240)

const checkpoints = await Promise.all(crt.getCheckpoints())

console.log(checkpoints.reduce(sum))

/// -- END of part one

await finishPartTwo

console.log(crt.getScreen())

/// -- END of part two
