namespace HostedServiceExtension.Example.Handlers.Commands;

using System.Buffers;

public static class CommandExtensions
{
    public static void WriteAndAdvanceOk(this IBufferWriter<byte> writer)
    {
        "ok"u8.CopyTo(writer.GetSpan(2));
        writer.Advance(2);
    }

    public static void WriteAndAdvanceOk(this IBufferWriter<byte> writer, ReadOnlySpan<byte> option)
    {
        var length = 3 + option.Length;
        var span = writer.GetSpan(length);
        "ok "u8.CopyTo(span);
        option.CopyTo(span[3..]);
        writer.Advance(length);
    }

    public static void WriteAndAdvanceNg(this IBufferWriter<byte> writer)
    {
        "ng"u8.CopyTo(writer.GetSpan(2));
        writer.Advance(2);
    }
}
