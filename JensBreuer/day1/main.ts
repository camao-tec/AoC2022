const LINE_SEPARATOR = "\n"

const plainText = await Deno.readTextFile("./input.txt")

const lines = plainText.split(LINE_SEPARATOR)

const elvesPackages = lines.reduce<string[][]>((
    accumulator, 
    itemInArray, 
    _indexInArray, 
    _entireArray
    ) => {
        if (itemInArray === "") {
            accumulator.push([])
        } else {
            accumulator.slice(-1)[0].push(itemInArray)
        }

        return accumulator
}, [[]])

const elvesPackagesSummary = elvesPackages.reduce<number[]>((
    accumulator, 
    itemInArray, 
    _indexInArray, 
    _entireArray
    ) => {
        accumulator.push(itemInArray.reduce<number>(
            (a, v, _i, _e) => {
                return a + parseInt(v)
            }, 0))
        return accumulator
    }, [])

const fattest = elvesPackagesSummary.reduce<{index: number, calories: number}>((
    accumulator, 
    itemInArray, 
    indexInArray, 
    _entireArray
    ) => {
        if (accumulator.calories < itemInArray) {
            return {index: indexInArray, calories: itemInArray}
        }
        return accumulator 
    }, { index: -1, calories: -1 } )

console.log(fattest)
console.log(elvesPackages[fattest.index])

/// -- END of part one

const final = elvesPackagesSummary
    .sort((a, b) => { return a - b })
    .slice(-3)
    .reduce((a, b) => {return a+b})

console.log("Top three accumulated: ", final)

/// -- END of part two