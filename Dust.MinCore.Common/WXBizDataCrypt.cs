using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Dust.MinCore.Common
{
    public class WXBizDataCrypt
    {
        public static dynamic DecryptData(string sessionKey, string iv, string encryptedData)
        {
            try
            {
                RijndaelManaged rijalg = new RijndaelManaged();
                rijalg.KeySize = 128;
                rijalg.Padding = PaddingMode.PKCS7;
                rijalg.Mode = CipherMode.CBC;
                rijalg.Key = Convert.FromBase64String(sessionKey);
                rijalg.IV = Convert.FromBase64String(iv);
                byte[] encryptedDataByte = Convert.FromBase64String(encryptedData);
                ICryptoTransform decryptor = rijalg.CreateDecryptor(rijalg.Key, rijalg.IV);
                string decryptResult = string.Empty;

                using (MemoryStream msDecrypt = new MemoryStream(encryptedDataByte))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decryptResult = srDecrypt.ReadToEnd();
                        }
                    }
                }
                var result = JsonConvert.DeserializeObject<dynamic>(decryptResult);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("非法数据" + ex.Message);
            }
        }
    }
}
