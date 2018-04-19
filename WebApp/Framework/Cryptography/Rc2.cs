using System;
using System.IO;
using System.Security.Cryptography;

namespace Framework.Cryptography
{
    public static class Rc2
    {
        private static byte[] InitialVector => System.Text.Encoding.Default.GetBytes("sdfgsdfydfgdfgdfgtrysdg");
        private static byte[] Key => System.Text.Encoding.Default.GetBytes("sdfgsfdsfgsdfgdg");

        private static string Salt => "dfdfgdfshdfhvbsdfasgsdfgsdfg";

        public static string Encrypt(string stringToBeEncrypted)
        {
            stringToBeEncrypted = Salt + stringToBeEncrypted;
            var bytesToBeEncrypted = System.Text.Encoding.Default.GetBytes(stringToBeEncrypted);
            var provider = new RC2CryptoServiceProvider();
            var encryptor = provider.CreateEncryptor(Key, InitialVector);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cryptoStream.FlushFinalBlock();
                }
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static string Decrypt(string encryptedText)
        {
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var provider = new RC2CryptoServiceProvider();
            var decrypter = provider.CreateDecryptor(Key, InitialVector);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                }

                return System.Text.Encoding.Default.GetString(memoryStream.ToArray()).Substring(Salt.Length);
            }
        }
    }
}