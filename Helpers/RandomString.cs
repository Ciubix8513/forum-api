namespace Api.Helpers;

public static class RandomString
{
    public static string RandomStr (int c)
    {
        Random random = new();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, c)
            .Select(_ => _[random.Next(_.Length)])
            .ToArray());
    }
}