namespace KeelteKoolV2.Core.Utils;

public static class StringUtils
{
    public static string ToCommaSepString(this List<string> list, string delim = ", ")
    {
        var ret = "";
        for (var i = 0; i < list.Count; i++)
        {
            ret += list[i];
            if (i < list.Count - 1)
                ret += delim;
        }

        return ret;
    }
}
