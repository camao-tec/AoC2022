import {
    assertEquals,
  } from "https://deno.land/std@0.167.0/testing/asserts.ts"
import {
    beforeAll,
    describe,
    it,
} from "https://deno.land/std@0.167.0/testing/bdd.ts"

import Graph from "npm:node-dijkstra@^2.5.0"

import { getAllPathsStartingAtA, getInputAsStringMatrix, getPath, getStartAndEnd, initGraph } from "./anc.ts"

describe("AoC Day 12", () => {
    describe("Dijkstra meets Hoffmann", () => {
        let inputMatrix: string[][]
        
        beforeAll(async () => {
            inputMatrix = await getInputAsStringMatrix("./input.txt.test")
        })

        it("should find the shortest path", () => {
            const route = new Graph()
            const matrix = JSON.parse(JSON.stringify(inputMatrix))
            const {0: start, 1:end} = getStartAndEnd(matrix)

            assertEquals(start, JSON.stringify([0, 0]))
            assertEquals(end, JSON.stringify([2, 5]))

            
            initGraph(matrix, route)
            
            const path = getPath(start, end, route)

            assertEquals(path.length - 1, 31)
        })

        it("should find the shortest path starting with any a", () => {
            const route = new Graph()
            const matrix = JSON.parse(JSON.stringify(inputMatrix))
            const {1:end} = getStartAndEnd(matrix)

            assertEquals(end, JSON.stringify([2, 5]))

            
            initGraph(matrix, route)
            
            const paths = getAllPathsStartingAtA(end, matrix, route)

            assertEquals(paths.map((p) => p.length - 1).reduce((a, b) => {
                if (a < b) {
                    return a
                }

                return b
            }, Infinity), 29)
        })
    })
})