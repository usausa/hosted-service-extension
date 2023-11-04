namespace HostedServiceExtension.Example.Handlers;

using System;
using System.Buffers;

public static class MemoryHelper
{
    public static bool SequentialEqual<T>(this ReadOnlySequence<T> sequence, ReadOnlySpan<T> span)
    {
        if (sequence.IsSingleSegment)
        {
            return sequence.FirstSpan.SequenceEqual(span);
        }

        foreach (var segment in sequence)
        {
            var length = segment.Length;
            if ((length > span.Length) || !segment.Span.SequenceEqual(span[..length]))
            {
                return false;
            }

            span = span[length..];
        }

        return span.Length == 0;
    }

    public static void Split<T>(ref ReadOnlySequence<T> buffer, out ReadOnlySequence<T> split, T delimiter)
        where T : unmanaged, IEquatable<T>
    {
        var reader = new SequenceReader<T>(buffer);
        if (reader.TryReadTo(out split, delimiter))
        {
            buffer = buffer.Slice(reader.Position);
        }
        else
        {
            split = buffer;
            buffer = ReadOnlySequence<T>.Empty;
        }
    }
}
