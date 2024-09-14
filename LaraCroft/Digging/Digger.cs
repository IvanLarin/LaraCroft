using LaraCroft.Entities;

namespace LaraCroft.Digging;

internal interface Digger
{
    Task Dig(Work<Candle>[] works, int timeframeInMinutes);
}