import { findSOMMarker, findSOPMarker, getInputLines } from "./aux.ts";

const seqs = await getInputLines("./input.txt")

console.log(seqs.map(findSOPMarker))

/// -- END of part one

console.log(seqs.map(findSOMMarker))

/// -- END of part two