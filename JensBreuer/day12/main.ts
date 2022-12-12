import Graph from "npm:node-dijkstra@^2.5.0"

import { getAllPathsStartingAtA, getInputAsStringMatrix, getPath, getStartAndEnd, initGraph } from "./anc.ts";


const matrix = await getInputAsStringMatrix("./input.txt")

const {0: start, 1:end} = getStartAndEnd(matrix)

const route = new Graph()

initGraph(matrix, route)

const path = getPath(start, end, route)

console.log(path.length - 1)

/// -- END of part one

const paths = getAllPathsStartingAtA(end, matrix, route)

console.log(paths
    .map((p) => p.length - 1)
    .reduce((a, b) => {
        if (a < b) {
            return a
        }

        return b
    }, Infinity
    )
)

/// -- END of part two
