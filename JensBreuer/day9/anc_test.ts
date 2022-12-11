import {
    assertEquals,
  } from "https://deno.land/std@0.167.0/testing/asserts.ts"
import {
    beforeAll,
    describe,
    it,
} from "https://deno.land/std@0.167.0/testing/bdd.ts"

import { getInputAsSequenceArray, HypotheticalSeriesOfMotions, Sequence, WeirdHypotheticalSeriesOfMotions } from "./anc.ts"

describe("AoC Day 9", () => {
    describe("At the end of the rope", () => {
        let seqs: Sequence[]
        
        beforeAll(async () => {
            seqs = await getInputAsSequenceArray("./input.txt.test")
        })

        it("should follow the steps", () => {
            const hsom = new HypotheticalSeriesOfMotions()
            
            hsom.runSeq(seqs[0])
            assertEquals(hsom.head, [0, 4])
            assertEquals(hsom.tail, [0, 3])
            assertEquals(hsom.tailTrail, [[0, 0], [0, 1], [0, 2], [0, 3]])

            hsom.runSeq(seqs[1])
            assertEquals(hsom.head, [4, 4])
            assertEquals(hsom.tail, [3, 4])
            assertEquals(hsom.tailTrail, [[0, 0], [0, 1], [0, 2], [0, 3],
                                            [1, 4], [2, 4], [3, 4]])
        })

        it("should calculate the number of positions the tail of the rope visited at least once", () => {
            const hsom = new HypotheticalSeriesOfMotions()
            hsom.runAllSeqs(seqs)
            assertEquals(hsom.tailTrail.length, 13)
        })
    })

    describe.only("At the 9th end of the rope", () => {
        let seqs: Sequence[]
        
        beforeAll(async () => {
            seqs = await getInputAsSequenceArray("./input.txt.test.2")
        })

        it("should calculate the number of positions the tail of the rope visited at least once", () => {
            const whsom = new WeirdHypotheticalSeriesOfMotions(9)
            whsom.runAllSeqs(seqs)
            assertEquals(whsom.tailTrail.length, 36)
        })

        it.only("should follow the steps", () => {
            const whsom = new WeirdHypotheticalSeriesOfMotions(9)

            whsom.runSeq(seqs[0])
            assertEquals(whsom.head, [0, 5])
            assertEquals(whsom.tail, [0, 4])
            assertEquals(whsom.tails, [
                [ 0, 3 ], [ 0, 2 ],
                [ 0, 1 ], [ 0, 0 ],
                [ 0, 0 ], [ 0, 0 ],
                [ 0, 0 ], [ 0, 0 ]
            ])
            console.log("tails: ", whsom.tails)

        })
    })
})