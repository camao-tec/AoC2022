const LINE_SEPARATOR = "\n"

const plainText = await Deno.readTextFile("./input.txt")

const assignmentPairs = plainText.split(LINE_SEPARATOR)

const sectionTuples = assignmentPairs.map((e: string) => {
    const {0: left, 1:right} = e.split(",")

    return [left.split("-"), right.split("-")].map((t) => { return [parseInt(t[0]), parseInt(t[1])] })
        .sort((t1, t2) => { return t1[0] - t2[0] })
})

const subsets = sectionTuples.filter((e) => {
    if (e[0][0] <= e[1][0] && e[1][1] <= e[0][1]) {
        return true
    }

    return false
})

console.log(subsets.length)

/// -- END of part one

const intersects = sectionTuples.filter((e) => {
    if (e[0][0] <= e[1][0] && e[1][0] <= e[0][1]) {
        return true
    }

    return false
})

console.log(intersects.length)

/// -- END of part two
