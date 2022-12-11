import { nextTick } from "https://deno.land/std@0.167.0/node/process.ts";

export const sum = (a: number, b: number) => { return a + b }

export class CRT {
    counter = 0
    executedAt = 0
    nextAX = 1
    AX = 1
    cmds: string[]
    cbs

    screen = [  "........................................".split(""),
                "........................................".split(""),
                "........................................".split(""),
                "........................................".split(""),
                "........................................".split(""),
                "........................................".split(""),
            ]

    constructor(cmds: string[]) {
        this.cmds = cmds
        this.cbs = new Map<number, (n: number) => void>()
        
        this.updateScreen()
        this.evaluate()
    }

    private updateScreen() {
        const row = Math.floor(this.counter / 40)
        const col = this.counter % 40

        if (col >= (this.AX - 1) && col <= (this.AX + 1)) {
            this.screen[row][col] = "#"
        }
    }

    public getCheckpoints() {
        const checkpoints = [20, 60, 100, 140, 180, 220]
        return checkpoints.map((c) => {
            return new Promise<number>((resolve) => {
                this.cbs.set(c, resolve)
            })
        })
    }

    public manualCheckpoint(n: number) {
        return new Promise<number>((resolve) => {
            this.cbs.set(n, resolve)
        })
    }

    public run() {
        nextTick(() => {
            this.execute()
        })
    }
    
    private execute() {
        this.updateScreen()

        this.counter++;

        this.resolve()
        
        this.evaluate()

        if(this.cbs.size > 0) {
            this.run()
        }
    }

    private evaluate() {
        if (this.counter == this.executedAt) {
            this.AX = this.nextAX
            const cmd = this.cmds.shift()?.split(" ") ?? []
            switch (cmd[0]) {
                case "noop": {
                    this.executedAt = this.counter + 1
                    this.nextAX = this.AX
                } break
                case "addx": {
                    this.executedAt = this.counter + 2
                    this.nextAX = this.AX + parseInt(cmd[1])
                } break
            }
        }
    }

    private resolve() {
        if (this.cbs.has(this.counter)) {
            const resolve = this.cbs.get(this.counter)
            if (resolve) {
                resolve(this.AX * this.counter)
                this.cbs.delete(this.counter)
            }
        }
    }

    public getScreen() {
        return this.screen.map((e) => e.join(""))
    }
}

export const getInputLines = async (fileName: string) => {
    const LINE_SEPARATOR = "\n"
    const plainText = await Deno.readTextFile(fileName)
    return plainText.split(LINE_SEPARATOR)
}