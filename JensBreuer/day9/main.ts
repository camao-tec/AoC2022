import { getInputAsSequenceArray, HypotheticalSeriesOfMotions } from "./anc.ts";

const seqs = await getInputAsSequenceArray("./input.txt")

const hsom = new HypotheticalSeriesOfMotions()
hsom.runAllSeqs(seqs)            
console.log(hsom.tailTrail.length)

/// -- END of part one

/// -- END of part two
