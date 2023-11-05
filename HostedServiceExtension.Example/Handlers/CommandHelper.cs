namespace HostedServiceExtension.Example.Handlers;

using System.Buffers;

public static class CommandHelper
{
    public static void WriteAndAdvanceOk(this IBufferWriter<byte> writer)
    {
        "ok\r\n"u8.CopyTo(writer.GetSpan(4));
        writer.Advance(4);
    }

    public static void WriteAndAdvanceOk(this IBufferWriter<byte> writer, ReadOnlySpan<byte> option)
    {
        "ok "u8.CopyTo(writer.GetSpan(3));
        writer.Advance(3);
        option.CopyTo(writer.GetSpan(option.Length));
        writer.Advance(option.Length);
        "\r\n"u8.CopyTo(writer.GetSpan(2));
        writer.Advance(2);
    }

    public static void WriteAndAdvanceNg(this IBufferWriter<byte> writer)
    {
        "ng\r\n"u8.CopyTo(writer.GetSpan(4));
        writer.Advance(4);
    }
}
