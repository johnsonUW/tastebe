using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Framework.Cryptography
{
    public static class Rsa
    {
        public static string Encrypt(string stringToBeEncrypted, string stringPublicKey)
        {
            var publicKeySequence = (DerSequence)Asn1Object.FromByteArray(Convert.FromBase64String(stringPublicKey));
            var encodedPublicKey1 = new DerBitString(publicKeySequence[0]);
            var encodedPublicKey2 = new DerBitString(publicKeySequence[1]);
            var modulus = (DerInteger)Asn1Object.FromByteArray(encodedPublicKey1.GetBytes());
            var exponent = (DerInteger)Asn1Object.FromByteArray(encodedPublicKey2.GetBytes());
            var y = new RsaKeyParameters(false, modulus.PositiveValue, exponent.PositiveValue);
            var x = DotNetUtilities.ToRSAParameters(y);
            using (var provider = new RSACryptoServiceProvider())
            {
                provider.ImportParameters(x);
                var dataToBeEncrypted = Encoding.UTF8.GetBytes(stringToBeEncrypted);
                var bytes = provider.Encrypt(dataToBeEncrypted, true);
                return Convert.ToBase64String(bytes);
            }
        }
    }
}