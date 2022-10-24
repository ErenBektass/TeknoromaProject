using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    public static class Crypt
    {
        private static string Key { get; set; } = "a?2_2/*/**-le21dn1jıondı1jd";
        public static string Decrypt(string cipher)
        {
            
            byte[] data = UTF8Encoding.UTF8.GetBytes(cipher);
            using (MD5CryptoServiceProvider md5=new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider()
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }

                
            }

                      
        }     
        public static string Encrypt(string text)
        {
            byte[] data = Convert.FromBase64String(text);

            using (MD5CryptoServiceProvider md5= new MD5CryptoServiceProvider())
            {
                byte [] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                {
                    Key=keys,
                    Mode=CipherMode.ECB,Padding=PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }

        







    }


}

