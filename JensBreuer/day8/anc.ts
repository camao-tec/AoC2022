const sum = (a: number, b: number) => { return a + b }

export const calculateVisibleTrees = (matrix: number[][]): number => {
    return matrix.map((row, rowIndex) => {
        return row.map((height, colIndex) => {
            if (rowIndex == 0 || rowIndex == matrix.length - 1
                || colIndex == 0 || colIndex == row.length - 1) {
                return 1
            }

            return walkthrough(matrix, [rowIndex, colIndex], height)
        }).reduce(sum)
    }).reduce(sum)
}

const gte = (a: number, b: number) => { return a >= b }
function walkthrough(matrix: number[][], [y, x]: [number, number], height: number): number {
    // east
    if (matrix[y].slice(x+1).find((e) => gte(e, height)) === undefined) {
        return 1
    }
    
    // west
    if (matrix[y].slice(0, x).find((e) => gte(e, height)) === undefined) {
        return 1
    }
    
    // north
    if (matrix.map((e) => e[x]).slice(0, y).find((e) => gte(e, height)) === undefined) {
        return 1
    }

    // south
    if (matrix.map((e) => e[x]).slice(y+1).find((e) => gte(e, height)) === undefined) {
        return 1
    }

    return 0
}

export const calculateHighestScenicScore = (matrix: number[][]): number => {
    return Math.max(...matrix.map((row, rowIndex) => {
        return Math.max(...row.map((height, colIndex) => {
            if (rowIndex == 0 || rowIndex == matrix.length - 1
                || colIndex == 0 || colIndex == row.length - 1) {
                return 0
            }

            return walkthroughScene(matrix, [rowIndex, colIndex], height)
        }))
    }))
}

function walkthroughScene(matrix: number[][], [y, x]: [number, number], height: number): number {
    const res: number[] = []

    // east
    const east = matrix[y].slice(x+1)
    const eastIndex = east.findIndex((e) => gte(e, height))
    res.push(eastIndex == -1 ? east.length : eastIndex + 1)
    
    // west
    const west = matrix[y].slice(0, x).reverse() // Thx, Florian!
    const westIndex = west.findIndex((e) => gte(e, height))
    res.push(westIndex == -1 ? west.length : westIndex + 1)
        
    // north
    const north = matrix.map((e) => e[x]).slice(0, y).reverse() // Thx, Florian!
    const northIndex = north.findIndex((e) => gte(e, height))
    res.push(northIndex == -1 ? north.length : northIndex + 1)

    // south
    const south = matrix.map((e) => e[x]).slice(y+1)
    const southIndex = south.findIndex((e) => gte(e, height))
    res.push(southIndex == -1 ? south.length : southIndex + 1)
    
    return res.reduce((a, b) => { return a * b })
}

export const getInputLines = async (fileName: string) => {
    const LINE_SEPARATOR = "\n"
    const plainText = await Deno.readTextFile(fileName)
    return plainText.split(LINE_SEPARATOR)
}

export const getInputAsMatrix = async (fileName: string) => {
    const lines = await getInputLines(fileName)

    return lines.map((l) => {
        return l.split("").map((c) => parseInt(c))
    })
}