namespace SystemModeling.Example.Extensions;

internal static class IntExtensions
{
    public static int SetBitsCount(this int value)
    {
        int count = 0;

        while (value != 0)
        {
            value &= value - 1;
            count++;
        }

        return count;
    }
}
