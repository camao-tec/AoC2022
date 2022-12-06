import {
    assertEquals,
  } from "https://deno.land/std@0.167.0/testing/asserts.ts"
import {
    beforeAll,
    describe,
    it,
} from "https://deno.land/std@0.167.0/testing/bdd.ts"

import { getInputLines, findSOPMarker, findSOMMarker, dive } from "./aux.ts"

describe("AoC Day 6", () => {
    describe("start-of-packet", () => {
        let seqs: string[] = []
        
        beforeAll(async () => {
            seqs = await getInputLines("./input.txt.test")
        })

        it("detects a start-of-packet marker", () => {
            const correctResults = [7, 5, 6, 10, 11]

            const results: number[] = seqs.map(findSOPMarker)

            assertEquals(results, correctResults)
        })

        it("detects a start-of-packet marker exactly at the end", () => {
            assertEquals(findSOPMarker("ffffuuufuck"), 11)
        })

        it("detects a start-of-packet marker exactly at the start", () => {
            assertEquals(findSOPMarker("fuckffffuuu"), 4)
        })
    })

    describe("start-of-message", () => {
        it("detects a start-of-message message", () => {
            const seqs = [
                            "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
                            "bvwbjplbgvbhsrlpgdmjqwftvncz",
                            "nppdvjthqldpwncqszvftbrmjlhg",
                            "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
                            "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"
                        ]
            const correctResults = [19, 23, 23, 29, 26]

            
            assertEquals(seqs.map(findSOMMarker), correctResults)
        })
    })

    describe("Dio - Holy Diver", () => {
        it("diverges", () => assertEquals(dive("ffu", 4), -1))
    })
})