type Point = [number, number] // [y, x]
type Movement = "R" | "L" | "U" | "D"
export type Sequence = [Movement, number]

export class HypotheticalSeriesOfMotions {
    head: Point = [0, 0]
    previousHead: Point = [0, 0]
    tail: Point = [0, 0]
    tailTrail: Point[] = [[0, 0]]

    

    public followTheWhiteRabbit(): void {
        //const diff: Point = [this.previousHead[0] - this.tail[0], this.previousHead[1] - this.tail[1]]
        //console.log("diff: ", diff)
        
        //this.tail[0] = this.tail[0] + diff[0]
        //this.tail[1] = this.tail[1] + diff[1]
        this.tail = this.previousHead
        this.tailTrail.find((p) => p[0] == this.tail[0] && p[1] == this.tail[1])
            ?? this.tailTrail.push(this.tail)
    }

    public runAllSeqs(seqs: Sequence[]) {
        seqs.forEach((s) => this.runSeq(s))
    }

    public runSeq(seq: Sequence) {
        console.log("runSeq: ", seq)
        for (let i = 0; i < seq[1]; i++) {
            this.move(seq[0])
        }
    }

    public move(m: Movement) {
        const [y, x] = this.head
        this.previousHead = this.head;
        switch(m) {
            case "D": {
                this.head = [y - 1, x]
            } break
            case "U": {
                this.head = [y + 1, x]
            } break
            case "L": {
                this.head = [y, x - 1]
            } break
            case "R": {
                this.head = [y, x + 1]
            } break
        }

        
        if (needsToMove(this.head, this.tail)) {
            this.followTheWhiteRabbit()
        }
    }
}


function distance(a: Point, b: Point) {
    return [Math.abs(a[0] - b[0]), Math.abs(a[1] - b[1])]
}

function needsToMove(head: Point, tail: Point) {
    const dist = distance(head, tail)
    
    return dist[0] > 1 || dist[1] > 1
}

export class WeirdHypotheticalSeriesOfMotions extends HypotheticalSeriesOfMotions {
    tails: Point[]

    constructor(c: number) {
        super()
        this.tails = new Array<Point>(c-1).map(() => [0, 0])
        for (let i = 0; i < this.tails.length; i++) {
            this.tails[i] = [0, 0]
        }
    }

    public followTheWhiteRabbit(): void {
        moveAll(this.tail, this.tails)

        this.tail = this.previousHead

        const realTail = this.tails.at(-1) ?? [0, 0]
        //console.log("tails: ", this.tails)
        this.tailTrail.find((p) => p[0] == realTail[0] && p[1] == realTail[1])
            ?? this.tailTrail.push(this.tail)
    }

}

function moveAll(c: Point, ts: Point[]) {
    const t = ts[0]
    if (t == undefined) return

    if (!needsToMove(c, t)) return

    const diff: Point = [c[0] - t[0], c[1] - t[1]]
    console.log("diff: ", diff)
    
    t[0] = t[0] + diff[0]
    t[1] = t[1] + diff[1]

    moveAll(t, ts.slice(1))
}

export const getInputLines = async (fileName: string) => {
    const LINE_SEPARATOR = "\n"
    const plainText = await Deno.readTextFile(fileName)
    return plainText.split(LINE_SEPARATOR)
}

export const getInputAsSequenceArray = async (fileName: string) => {
    const lines = await getInputLines(fileName)

    return lines.map<Sequence>((l) => {
        const pair = l.split(" ")
        return [<Movement>pair[0], parseInt(pair[1])]
    })
}