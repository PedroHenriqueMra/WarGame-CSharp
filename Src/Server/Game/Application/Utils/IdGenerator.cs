using System.Threading;

public static class IdGenerator
{
    private static int _roomIds;

    public static int GenRoomId()
        => Interlocked.Increment(ref _roomIds);

}
