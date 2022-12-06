const LINE_SEPARATOR = "\n"

const plainText = await Deno.readTextFile("./input.txt")

const lines = plainText.split(LINE_SEPARATOR)

const divider = lines.indexOf("");

const stackInit = lines[divider - 1] ?? "0"
const rawStackData = lines.slice(0, divider - 1).reverse()
const instructions = lines.slice(divider + 1)

const stacks: string[][] = []
stackInit.split(" ").map((e) => stacks[parseInt(e)] = [])

rawStackData.forEach((e) => {
    const parts = e.match(/.{1,4}/g)

    parts?.forEach((l, i) => {
        const crate = l.charAt(1)
        if(crate !== " ") {
            stacks[i + 1].push(crate)
        }
    })
})

instructions.forEach((instruction) => {
    const { 1: howMany, 3: from, 5: to } = instruction.split(" ")
    stacks[parseInt(to)]
        .push(...stacks[parseInt(from)]
            .splice(-parseInt(howMany))
            //.reverse() // <- that's part two config. remove comment at the beginning to get part one config ;-)
        )
})

const tips = stacks.splice(1).reduce<string>((a, e) => { return a + e.at(-1) }, "")

console.log(tips)
