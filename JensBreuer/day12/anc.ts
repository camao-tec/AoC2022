import Graph from "npm:node-dijkstra@^2.5.0"

type Point = [number, number]

export const initGraph = (matrix: string[][], route: typeof Graph) => {
    matrix.forEach((row, y) => {
        row.forEach((_col, x) => {
            const u = matrix[y][x].charCodeAt(0) + 1
            const stash: Point[] = []
            // north
            if (y > 0) {
                const v = matrix[y-1][x].charCodeAt(0)
                if (v <= u) {
                    stash.push([y-1, x])
                }
            }
            
            // south
            if (y < matrix.length - 1) {
                const v = matrix[y+1][x].charCodeAt(0)
                if (v <= u) {
                    stash.push([y+1, x])
                }
            }

            // east
            if (x < row.length - 1) {
                const v = matrix[y][x+1].charCodeAt(0)
                if (v <= u) {
                    stash.push([y, x+1])
                }
            }

            // west
            if (x > 0) {
                const v = matrix[y][x-1].charCodeAt(0)
                if (v <= u) {
                    stash.push([y, x-1])
                }
            }

            if (stash.length > 0) {
                const n = new Map()
                stash.map((e) => { n.set(JSON.stringify(e), 1) })
                route.addNode(JSON.stringify([y, x]), n)
            }
        })
    })
}

export const getStartAndEnd = (matrix: string[][]): string[] => {
    // TODO: set S=a && E=z
    const res: string[] = [];

    matrix.forEach((row, y) => {
        row.forEach((_col, x) => {
            if (matrix[y][x] == "S") {
                res[0] = JSON.stringify([y, x])
                matrix[y][x] = "a"
            } else if (matrix[y][x] == "E") {
                res[1] = JSON.stringify([y, x])
                matrix[y][x] = "z"
            }

            if (res.length == 2) {
                return res
            }
        })
    })

    return res
}

export const getPath = (start: string, end: string, route: typeof Graph) => {
    return route.path(start, end)
}

export const getAllPathsStartingAtA = (end: string, matrix: string[][], route: typeof Graph) => {
    const as: string[] = []
    matrix.forEach((row, y) => {
        row.forEach((_col, x) => {
            if (matrix[y][x] == "a") {
                as.push(JSON.stringify([y, x]))
            }
        })
    })

    return as.map((a) => route.path(a, end)).filter((a) => a != null)
}

export const getInputLines = async (fileName: string) => {
    const LINE_SEPARATOR = "\n"
    const plainText = await Deno.readTextFile(fileName)
    return plainText.split(LINE_SEPARATOR)
}

export const getInputAsStringMatrix = async (fileName: string) => {
    return (await getInputLines(fileName)).map((e) => e.split(""))
}