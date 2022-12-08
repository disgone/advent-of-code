var dataStream = File.ReadAllText("input.txt");

int GetMarkerPosition(ReadOnlySpan<char> text, int length)
{
    bool HasDuplicate(char needle, ReadOnlySpan<char> haystack)
    {
        foreach (var token in haystack)
        {
            if (needle == token) return true;
        }

        return haystack.Length > 1 && HasDuplicate(haystack[0], haystack.Slice(1));
    }
    
    for (int i = 0; i < text.Length - length; i++)
    {
        var window = text.Slice(i, length);

        if (!HasDuplicate(window[0], window.Slice(1)))
        {
            return i + length;
        }
    }

    throw new InvalidOperationException("Could not find unique marker");
}

int markerPosition = GetMarkerPosition(dataStream.AsSpan(), 14);

Console.WriteLine($"first marker after character {markerPosition}");