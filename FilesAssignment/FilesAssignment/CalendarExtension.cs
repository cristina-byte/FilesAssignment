
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FilesAssignment
{
     internal static class CalendarExtension
    {
        public static byte[] EncryptStringToBytes(this Calendar c,string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

       public static string DecryptStringFromBytes(this Calendar c,byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

           
            string plaintext = null;
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

        public static void CompressFile(this Calendar c,string originalFileName, string compressedFileName)
        {
            //Get the length of the original file
            FileInfo fileInfo = new FileInfo(originalFileName);
            Console.WriteLine("\nThe length before compression:" + fileInfo.Length);

            FileStream originalFileStream = File.Open(originalFileName, FileMode.Open);
            FileStream compressedFileStream = File.Create(compressedFileName);
            GZipStream gzipStream = new GZipStream(compressedFileStream, CompressionMode.Compress,false);
            originalFileStream.CopyTo(gzipStream);
            gzipStream.Close();
            
            //Get the length of the compressed file
            FileInfo fileInfo2 = new FileInfo(compressedFileName);
            Console.WriteLine("\nThe length after compression:" + fileInfo2.Length);
        }
    }
}
