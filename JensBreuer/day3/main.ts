const LINE_SEPARATOR = "\n"

const plainText = await Deno.readTextFile("./input.txt")

const diggnsäx = plainText.split(LINE_SEPARATOR)

const UPPERCASE_ASCII_OFFSET = 64
const LOWERCASE_ASCII_OFFSET = 96
const UPPERCASE_PRIORITY_ADJUSTMENT = 26 
const toPriority = (l: string) => {
    if (l >= "a" && l <= "z") {
        return l.charCodeAt(0) - LOWERCASE_ASCII_OFFSET
    } else if (l >= "A" && l <= "Z") {
        return l.charCodeAt(0) - UPPERCASE_ASCII_OFFSET + UPPERCASE_PRIORITY_ADJUSTMENT
    } else {
        throw new Deno.errors.Interrupted("SNAFU: invalid char - " + l)
    }
}

const priorities: number[] = diggnsäx.map((e: string) => {
    const first = e.slice(0, e.length/2)
    const second = e.slice(e.length/2)
    
    const dupe = [...first].find((c: string) => { return second.includes(c) })

    if (dupe === undefined) throw new Deno.errors.Interrupted("SNAFU: no dupe found")

    return toPriority(dupe)
});

const sum = (a: number, b: number) => { return a + b }

console.log(priorities.reduce(sum))

/// -- END of part one

const triplets = diggnsäx.reduce<string[][]>((a, b, _c, _d) => {
    const lastElement = a.slice(-1)[0]
    if (lastElement.length === 3) {
        a.push([b])
    } else {
        lastElement.push(b)
    }
    return a
}, [[]])

const badges = triplets.map((e) => {
    const {0: one, 1: two, 2: three} = e

    return [...one].find((c) => {
        return two.includes(c) && three.includes(c)
    }) ?? ""
})

console.log(badges.map(toPriority).reduce(sum))

/// -- END of part two
