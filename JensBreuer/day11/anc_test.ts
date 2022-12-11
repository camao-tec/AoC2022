import {
    assertEquals,
  } from "https://deno.land/std@0.167.0/testing/asserts.ts"
import {
    beforeAll,
    describe,
    it,
} from "https://deno.land/std@0.167.0/testing/bdd.ts"

import { CrazyMonkey, getInputMonkeyChunks, Monkey, MonkeyOnMyBackpack } from "./anc.ts"

describe("AoC Day 11", () => {
    describe("Monkey on my Backpack", () => {
        let monkeyChunks: string[][]
        
        beforeAll(async () => {
            monkeyChunks = await getInputMonkeyChunks("./input.txt.test")
        })

        it("should throw stuff around", async () => {
            const monkeys = monkeyChunks.map((monkeyChunk) => {
                return new Monkey(monkeyChunk)
            })

            const momb = new MonkeyOnMyBackpack(monkeys)
            momb.run()

            const checkpoints = []
            for (let i = 1; i <= 10; i++) {
                checkpoints.push(momb.manualCheckpoint(i))
            }
            checkpoints.push(momb.manualCheckpoint(15))
            checkpoints.push(momb.manualCheckpoint(20))

            const monkeyData = await Promise.all(checkpoints)

            assertEquals(monkeyData[0], [[20, 23, 27, 26], [2080, 25, 167, 207, 401, 1046], [], []])

            assertEquals(monkeyData[1], [[695, 10, 71, 135, 350], [43, 49, 58, 55, 362], [], []])

            assertEquals(monkeyData[2], [[16, 18, 21, 20, 122], [1468, 22, 150, 286, 739], [], []])

            assertEquals(monkeyData[3], [[491, 9, 52, 97, 248, 34], [39, 45, 43, 258], [], []])

            assertEquals(monkeyData[4], [[15, 17, 16, 88, 1037], [20, 110, 205, 524, 72], [], []])

            assertEquals(monkeyData[5], [[8, 70, 176, 26, 34], [481, 32, 36, 186, 2190], [], []])

            assertEquals(monkeyData[6], [[162, 12, 14, 64, 732, 17], [148, 372, 55, 72], [], []])

            assertEquals(monkeyData[7], [[51, 126, 20, 26, 136], [343, 26, 30, 1546, 36], [], []])

            assertEquals(monkeyData[8], [[116, 10, 12, 517, 14], [108, 267, 43, 55, 288], [], []])

            assertEquals(monkeyData[9], [[91, 16, 20, 98], [481, 245, 22, 26, 1092, 30], [], []])

            assertEquals(monkeyData[10], [[83, 44, 8, 184, 9, 20, 26, 102], [110, 36], [], []])

            assertEquals(monkeyData[11], [[10, 12, 14, 26, 34], [245, 93, 53, 199, 115], [], []])

            assertEquals(momb.monkeyBusinessLevel(), 10605)
        })

        it("should go crazy", async () => {
            const crazyMonkeys = monkeyChunks.map((monkeyChunk) => {
                return new CrazyMonkey(monkeyChunk)
            })
            
            const cmomb = new MonkeyOnMyBackpack(crazyMonkeys)
            cmomb.run()
            
            await cmomb.manualCheckpoint(10000)
            
            assertEquals(cmomb.monkeyBusinessLevel(), 2713310158)
        })
    })
})