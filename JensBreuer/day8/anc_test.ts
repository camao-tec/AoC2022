import {
    assertEquals,
  } from "https://deno.land/std@0.167.0/testing/asserts.ts"
import {
    beforeAll,
    describe,
    it,
} from "https://deno.land/std@0.167.0/testing/bdd.ts"

import { calculateHighestScenicScore, calculateVisibleTrees, getInputAsMatrix } from "./anc.ts"

describe("AoC Day 7", () => {
    describe("Missing in Treehouse", () => {
        let matrix: number[][]
        
        beforeAll(async () => {
            matrix = await getInputAsMatrix("./input.txt.test")
        })

        it("should calculate the trees visible in total", () => {
            assertEquals(calculateVisibleTrees(matrix), 21)
        })

        it("should calculate the highest scenic score possible", () => {
            assertEquals(calculateHighestScenicScore(matrix), 8)
        })
    })
})