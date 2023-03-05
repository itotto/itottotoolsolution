using System.Text;
using System.Security.Cryptography;

namespace alertdiff {
    /// <summary>
    /// ハッシュ計算
    /// </summary>
    public static class CalcHashcs {
        /// <summary>
        /// MD5でハッシュを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetMD5Hash(string data) {
            if (string.IsNullOrEmpty(data)) return string.Empty;

            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.Unicode.GetBytes(data));

            return Convert.ToBase64String(hash);
        }
    }
}
