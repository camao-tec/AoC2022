import { getInputLines } from "./aux.ts"

const sum = (a: number, b: number) => { return a + b }

class File {
    name = ""
    size = 0
}

class Directory extends File {
    dirs: Directory[] = []
    files: File[] = []
}

class Reality {
    cwd: Directory[]
    root: Directory

    public constructor() {
        this.cwd = []
        this.root = new Directory()
        this.root.name = "/"
    }

    public pwd() {
        return this.cwd.slice(-1)[0]
    }

    public setRootCwd() {
        this.cwd = [this.root]
    }

    public cdDotDot() {
        reality.cwd.pop() ?? reality.setRootCwd()
    }

    public du() {
        function holyDiver(dir: Directory): number {
            if (dir.dirs.length > 0) {
                dir.size += dir.dirs.map((d) => holyDiver(d)).reduce(sum)
            }
            
            if (dir.files.length > 0) {
                dir.size += dir.files.map((f) => f.size).reduce(sum)
            }


            return dir.size
        }

        holyDiver(this.root)
    }

    public omgitsover100000() {
        function findIt(d: Directory): Directory[] {
            if (d.size <= 100000) return [d, ...d.dirs.flatMap((d) => findIt(d))]

            return d.dirs.flatMap((d) => findIt(d))
        }

        return findIt(this.root)
    }

    public omgitsaround30000000() {
        const currentlyUsed = 70000000 - this.root.size
        const needed = 30000000 - currentlyUsed
        function findThem(d: Directory): Directory[] {
            if (d.size >= needed) return [d, ...d.dirs.flatMap((d) => findThem(d))]

            return d.dirs.flatMap((d) => findThem(d))
        }

        return findThem(this.root).sort((a,b) => b.size - a.size)
    }
}

const sequence = await getInputLines("./input.txt")

const reality = new Reality()

sequence.forEach((l) => {
    if (l.startsWith("$")) {
        const parts = l.split(" ")
        const cmd = parts[1];
        switch (cmd) {
            case "cd": {
                const name = parts[2]
                if ( name == "..") {
                    reality.cdDotDot()
                } else if (name == "/") {
                    reality.setRootCwd()
                } else {
                    const pwd = reality.pwd()
                    const dir = pwd.dirs.find((dir) => dir.name == name)
                    if (dir == undefined) throw new Deno.errors.Interrupted("dir not found: " + name)
                    reality.cwd.push(dir)
                }
            } break
            case "ls": {
                // nothing to do here... NOP NOP NOP
            } break
        }
    } else {
        const {0: left, 1: right} = l.split(" ")
        if (left == "dir") {
            const dir = new Directory()
            dir.name = right
            reality.pwd().dirs.push(dir)
        } else {
            const file = new File()
            file.name = right
            file.size = parseInt(left)
            reality.pwd().files.push(file)
        }
    }
})

reality.du()

console.log(reality.omgitsover100000().map((d) => d.size).reduce(sum))

/// -- END of part one

console.log(reality.omgitsaround30000000().at(-1)?.size)

/// -- END of part two