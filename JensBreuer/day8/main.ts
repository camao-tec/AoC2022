import { calculateHighestScenicScore, calculateVisibleTrees, getInputAsMatrix } from "./anc.ts";

const matrix = await getInputAsMatrix("./input.txt")


/// -- END of part one

console.log(calculateVisibleTrees(matrix))

/// -- END of part two

console.log(calculateHighestScenicScore(matrix))