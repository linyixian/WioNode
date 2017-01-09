using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using System.Net.Http;
using Newtonsoft.Json;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace WioNode
{
    public class TMP
    {
        public float celsius_degree { get; set; }
    }

    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //測定間隔用タイマー
        DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            //タイマーのセット（10秒ごとに温度を取得）
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, object e)
        {
            
            var client = new HttpClient();
            string contents;

            //1Fに設置したセンサーの値を取得
            contents = await client.GetStringAsync("https://us.wio.seeed.io/v1/node/GroveTempHumD0/temperature?access_token={AccessToken}");

            var Tmp_1F = JsonConvert.DeserializeObject<TMP>(contents);

            tBlock_1F.Text = "1Fの温度 : " + Tmp_1F.celsius_degree.ToString("F1");

            //2Fに設置したセンサーの値を取得
            contents = await client.GetStringAsync("https://us.wio.seeed.io/v1/node/GroveTempHumProD0/temperature?access_token={AccessToken}");

            var Tmp_2F = JsonConvert.DeserializeObject<TMP>(contents);

            tBlock_2F.Text = "2Fの温度 : " + Tmp_2F.celsius_degree.ToString("F1");

        }
    }
}
