using System.Security.Cryptography;

namespace Api.Helpers;

public static class Hasing
{
    public static string Hash(this string Password, string Salt) => new( SHA256.HashData((Password += Salt).Select(_ => (byte)_).ToArray()).Select(_ => (char)_).ToArray());
}