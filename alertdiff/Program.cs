using alertdiff;
using System.Text;

//*** 設定値を取得 ***//
// 処理間隔
var waitInterval = AppConfigurations.GetWaitInterval() * 1000;

// URIを取得
var uri = AppConfigurations.GetUri();

// アクセストークン
var accessToken = AppConfigurations.GetAccessToken();

// 通知メッセージ
var notifyMessage = AppConfigurations.GetNotifyMessage();
//*** 設定値ここまで ***//

// 比較対象のHTML(ここから変更があったら通知する)
const string ORIGINALHTML = ".\\original.html";

// 比較する基準となるハッシュ値
var originalHash = string.Empty;
if (File.Exists(ORIGINALHTML)) {
    var originalHtml = File.ReadAllText(ORIGINALHTML, Encoding.UTF8);
    originalHash = CalcHashcs.GetMD5Hash(originalHtml);
}

// 延々とチェック
while (true) {
    // HTMLを取得
    var html = await HttpUtility.GetHtmlBody(uri);

    // 新しいハッシュ値
    var newHash = CalcHashcs.GetMD5Hash(html);

    if (string.IsNullOrEmpty(originalHash)) {
        originalHash = newHash;
        File.WriteAllText(ORIGINALHTML, html);
        Console.WriteLine("比較対象の基準データがなかったので作成します");

    } else {
        var isSame = originalHash == newHash;

        // 結果を表示
        Console.WriteLine($"{(isSame ? "〇" : "×")} - original:{originalHash}\tnew:{newHash} ({DateTime.Now:yyyy/MM/dd HH:mm:ss})");

        // ページが更新されていたら通知
        if (!isSame) {
            var message = $"{notifyMessage} - {uri}";
            var r = await HttpUtility.PublishhMessage(message, accessToken);
            if (r) return;
            Console.WriteLine("通知に失敗したので再度チェックします");
        }
    }
    Task.Delay(waitInterval).Wait();
}
