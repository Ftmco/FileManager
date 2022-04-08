namespace File.Extension.Code;

public static class TokenExtension
{
    public static string CreateToken(this int length)
    {
        string newStr = Guid.NewGuid().ToString().Replace("-", "");
        while (newStr.Length < length)
            newStr += Guid.NewGuid().ToString().Replace("-", "");
        return newStr;
    }
}