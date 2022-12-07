export const getInputLines = async (fileName: string) => {
    const LINE_SEPARATOR = "\n"
    const plainText = await Deno.readTextFile(fileName)
    return plainText.split(LINE_SEPARATOR)
}

export const findSOPMarker = (e: string) => {
    return dive(e, 0)
}

export const findSOMMarker = (e: string) => {
    return dive(e, 0, 14)
}

export function dive(s: string, i: number, size = 4): number {
    if ((new Set(s.substring(i, i+size))).size === size) {
        return i+size
    }

    if (i < s.length - size) {
        return dive(s, i+1, size)
    }

    return -1
}