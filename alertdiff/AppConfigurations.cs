using Microsoft.Extensions.Configuration;

namespace alertdiff {
    public static class AppConfigurations {
        /// <summary>
        /// 設定ファイルからURIを取得
        /// </summary>
        /// <returns></returns>
        public static string GetUri() => Get("Configuration:Uri");

        /// <summary>
        /// アクセストークン
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken() => Get("Configuration:AccessToken");

        /// <summary>
        /// 通知メッセージ
        /// </summary>
        /// <returns></returns>
        public static string GetNotifyMessage() => Get("Configuration:NotifyMessage");

        /// <summary>
        /// 実行インターバル
        /// </summary>
        /// <returns></returns>
        public static int GetWaitInterval() => Convert.ToInt32(Get("Configuration:WaitInterval"));

        /// <summary>
        /// 設定から指定のキーの値を取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        private static string Get(string key) {
            const string JSONPATH = ".\\appsettings.json";

            if (!File.Exists(JSONPATH)) throw new FileNotFoundException(JSONPATH);

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(JSONPATH);

            var config = builder.Build();
            return config?[$"{key}"] ?? string.Empty;
        }
    }
}
