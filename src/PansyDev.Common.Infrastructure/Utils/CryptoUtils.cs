using System;
using System.Security.Cryptography;

namespace PansyDev.Common.Infrastructure.Utils
{
    public static class CryptoUtils
    {
        public static byte[] GenerateBytes(int size) => RandomNumberGenerator.GetBytes(size);

        public static string GenerateString(int size)
        {
            return BitConverter.ToString(GenerateBytes(size)).Replace("-", "");
        }
    }
}
