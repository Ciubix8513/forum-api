using System.Security.Cryptography;

namespace Api.Helpers;

public static class Hasing
{
    public static string Hash(string Password, string Salt) => SHA256.HashData((Password += Salt).Select(_ => (byte)_).ToArray()).ToString();
}