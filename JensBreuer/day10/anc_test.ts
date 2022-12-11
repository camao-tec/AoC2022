import {
    assertEquals,
  } from "https://deno.land/std@0.167.0/testing/asserts.ts"
import {
    beforeAll,
    describe,
    it,
} from "https://deno.land/std@0.167.0/testing/bdd.ts"

import { CRT, getInputLines, sum } from "./anc.ts"

describe("AoC Day 10", () => {
    describe("CRT", () => {
        let cmds: string[]
        
        beforeAll(async () => {
            cmds = await getInputLines("./input.txt.test")
        })

        it("should calculate the frequencies", async () => {
            const crt = new CRT(new Array(...cmds))

            crt.run()

            const checkpoints = await Promise.all(crt.getCheckpoints())

            assertEquals(checkpoints, [420, 1140, 1800, 2940, 2880, 3960])
            assertEquals(checkpoints.reduce(sum), 13140)
        })

        const screen = ["##..##..##..##..##..##..##..##..##..##..",
                        "###...###...###...###...###...###...###.",
                        "####....####....####....####....####....",
                        "#####.....#####.....#####.....#####.....",
                        "######......######......######......####",
                        "#######.......#######.......#######....."]
        
        it("should draw stuff", async () => {
            const crt = new CRT(new Array(...cmds))

            crt.run()

            await crt.manualCheckpoint(240)

            assertEquals(crt.getScreen(), screen)
        })
    })
})