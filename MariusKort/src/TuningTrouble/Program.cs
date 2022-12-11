const byte PACKET_MARKER_LENGTH = 4;
const byte MESSAGE_MARKER_LENGTH = 14;

var input = await File.ReadAllTextAsync("input.txt");

// part 1
int packetMarkerIndex = GetMarkerIndex(input, PACKET_MARKER_LENGTH);
Console.WriteLine($"start of packet after: {packetMarkerIndex}");

// part 2
var messsageMarkerIndex = GetMarkerIndex(input, MESSAGE_MARKER_LENGTH);
Console.WriteLine($"start of message after: {messsageMarkerIndex}");

static int GetMarkerIndex(string input, byte markerLength)
{
    var index = Enumerable.Range(0, input.Length - markerLength)
        .TakeWhile(i => input.Skip(i).Take(markerLength).Distinct().Count() < markerLength)
        .Last();

    return index + markerLength + 1;
}