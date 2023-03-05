using System.Net;
using System.Net.Http.Headers;

namespace alertdiff {
    public static class HttpUtility {
        /// <summary>
        /// HTTPアクセス(リソース不足対応)
        /// </summary>
        private static HttpClient _client = new HttpClient();

        /// <summary>
        /// HTMLを取得
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async static Task<string> GetHtmlBody(string uri) {
            using var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// LINEの通知を送信
        /// </summary>
        /// <param name="message"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static async Task<bool> PublishhMessage(string message, string access_token) {

            // 通知するメッセージ
            var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "message", message }, });

            // ヘッダーにアクセストークンを追加
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            // 実行
            var result = await _client.PostAsync("https://notify-api.line.me/api/notify", content);
            return result.StatusCode == HttpStatusCode.OK;
        }
    }
}
