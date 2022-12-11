import { nextTick } from "https://deno.land/std@0.167.0/node/process.ts";

export const sum = (a: number, b: number) => { return a + b }

export class Monkey {
    id: number
    items: number[]
    op!: (worryLevel: number) => number
    testAndThrow: (worryLevel: number) => void
    monkeys: Monkey[] = []
    inspect = 0

    constructor(monkeyChunk: string[]) {
        this.id = parseInt(monkeyChunk[0].split(" ")[1])

        this.items = monkeyChunk[1].split(":")[1].split(",").map((n) => parseInt(n))

        const {1:op, 2:right} = monkeyChunk[2].split("=")[1].trim().split(" ")
        
        switch(op) {
            case "*": {
                switch(right) {
                    case "old": {
                        this.op = (worryLevel: number) => {
                            return worryLevel * worryLevel
                        }
                    } break
                    default: {
                        this.op = (worryLevel: number) => {
                            return worryLevel * parseInt(right)
                        }
                    } break
                }
            } break
            case "+": {
                this.op = (worryLevel: number) => {
                    return worryLevel + parseInt(right)
                }
            } break
            default: {
                throw new Deno.errors.Interrupted("unknwon op: " + op)
            }
        }
        
        const divisibleBy = parseInt(monkeyChunk[3].split("by")[1])
        const monkeyIfTrue = parseInt(monkeyChunk[4].split("monkey")[1])
        const monkeyIfFalse = parseInt(monkeyChunk[5].split("monkey")[1])
        this.testAndThrow = (worryLevel: number) => {
            if (worryLevel % divisibleBy === 0) {
                this.monkeys.at(monkeyIfTrue)?.items.push(worryLevel)
            } else {
                this.monkeys.at(monkeyIfFalse)?.items.push(worryLevel)
            }
        }
    }

    public inspectAndAdapt() {
        if (this.items.length <= 0) {
            return
        }

        do {
            let worryLevel = this.items.shift() ?? 0
            worryLevel = this.op(worryLevel)

            const normalizedWorryLevel = Math.floor(worryLevel / 3)
            this.testAndThrow(normalizedWorryLevel)

            this.inspect++;
        } while (this.items.length > 0)
    }
}

export class CrazyMonkey extends Monkey {
    public inspectAndAdapt() {
        if (this.items.length <= 0) {
            return
        }

        do {
            let worryLevel = this.items.shift() ?? 0
            worryLevel = this.op(Math.floor(worryLevel/2))

            this.testAndThrow(worryLevel)

            this.inspect++;
        } while (this.items.length > 0)
    }
}

export class MonkeyOnMyBackpack {
    round = 0
    monkeys: Monkey[]
    cbs

    constructor(monkeys: Monkey[]) {
        this.monkeys = monkeys
        monkeys.forEach((monkey) => {
            monkey.monkeys = monkeys
        })

        this.cbs = new Map<number, (n: number[][]) => void>()
    }

    public manualCheckpoint(n: number) {
        return new Promise<number[][]>((resolve) => {
            this.cbs.set(n, resolve)
        })
    }

    public run() {
        nextTick(() => {
            this.execute()
        })
    }
    
    private execute() {
        this.round++;

        this.evaluate()
        this.resolve()

        if(this.cbs.size > 0) {
            this.run()
        }
    }

    private evaluate() {
        this.monkeys.forEach((monkey) => {
            monkey.inspectAndAdapt()
        })
    }

    private resolve() {
        if (this.cbs.has(this.round)) {
            const resolve = this.cbs.get(this.round)
            if (resolve) {
                resolve(JSON.parse(JSON.stringify(this.monkeys.map((m) => m.items))))
                this.cbs.delete(this.round)
            }
        }
    }

    public monkeyBusinessLevel() {
        console.log("inpect: ", this.monkeys.map((m) => m.inspect))
        return this.monkeys.map((m) => m.inspect).sort((a, b) => b - a).slice(0, 2).reduce((a, b) => a * b)
    }
}

export const getInputLines = async (fileName: string) => {
    const LINE_SEPARATOR = "\n"
    const plainText = await Deno.readTextFile(fileName)
    return plainText.split(LINE_SEPARATOR)
}

export const getInputMonkeyChunks = async (fileName: string) => {
    const lines = await getInputLines(fileName)
    return lines.reduce<string[][]>((a, b) => {
        if (b == "") {
            a.push([])
        } else {
            a.at(-1)?.push(b.trim())
        }

        return a
    }, [[]])
}