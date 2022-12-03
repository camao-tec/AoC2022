const LINE_SEPARATOR = "\n"

const plainText = await Deno.readTextFile("./input.txt")

const matches = plainText.split(LINE_SEPARATOR)

const lookupTable = new Map<string, number>([
    ["A", 1], // ROCK
    ["B", 2], // PAPER
    ["C", 3], // SCISSORS
    ["X", 1], // ROCK
    ["Y", 2], // PAPER
    ["Z", 3]  // SCISSORS
])

const LOST  = 0
const DRAW  = 3
const WIN   = 6

// Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock.
const win = (them: number, me: number) => {
    if (me == 1 && them == 3) {
        return true;
    } else if (me == 3 && them == 2) {
        return true;
    } else if (me == 2 && them == 1) {
        return true;
    }

    return false;
}


const evalMatch = (e: string) => {
    const parts = e.split(" ")
    const them = lookupTable.get(parts[0]) ?? 0
    const me = lookupTable.get(parts[1]) ?? 0
    
    if (them == me) {
        return DRAW + me
    }

    if (win(them, me)) {
        return WIN + me
    }

    return LOST + me
}

const totalScore = matches.map(evalMatch).reduce((a, b) => { return a + b })

console.log("totalScore: ", totalScore)

/// -- END of part one

// X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win
const goFigure = (e: string) => {
    const {0: them, 1: result} = e.split(" ")

    if (result == "X") {
        switch (them) {
            case "A":
                return "A Z"
            case "B":
                return "B X"
            case "C":
                return "C Y"
        }
    }

    if (result == "Y") {
        switch (them) {
            case "A":
                return "A X"
            case "B":
                return "B Y"
            case "C":
                return "C Z"
        }
    }

    if (result == "Z") {
        switch (them) {
            case "A":
                return "A Y"
            case "B":
                return "B Z"
            case "C":
                return "C X"
        }
    }

    throw new Deno.errors.BadResource("SNAFU - Unreachable code reached")
}

const realTotalScore = matches.map(goFigure).map(evalMatch).reduce((a, b) => { return a + b })

console.log("realTotalScore: ", realTotalScore)

/// -- END of part two