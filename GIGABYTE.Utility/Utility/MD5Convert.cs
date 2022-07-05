using System.Security.Cryptography;
using System.Text;

namespace GIGABYTE.Utility.Utility
{
    public static class MD5Convert
    {
        public static string GetMd5String(string privateKey, string input)
        {
            var md5 = GetMd5String(Encoding.UTF8.GetBytes(String.Format("{0}{1}", privateKey, input)));            
            return md5;
        }


        public static string GetMd5String(byte[] photoByte){
            var md5 = "";
            using (var cryptoMD5 = MD5.Create())
            {

                //取得雜湊值位元組陣列
                var hash = cryptoMD5.ComputeHash(photoByte);

                //取得 MD5
                md5 = BitConverter.ToString(hash)
                    .Replace("-", string.Empty)
                    .ToUpper();
            }
            return md5;
        }
    }
}
