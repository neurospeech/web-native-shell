using NativeShell.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeShell.Pages
{
    public class NativeShellMainPage: ContentPage
    {

        public readonly NativeWebView WebView;

        public string? Url { get; set; }

        public NativeShellMainPage()
        {
            WebView = new NativeWebView() {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            var grid = new Grid();
            grid.Children.Add(WebView);
            this.Content = grid;
            // this.Content = WebView;
            Dispatcher.DispatchTaskDelayed(TimeSpan.FromMilliseconds(1), this.Ask);
        }

        private async Task Ask()
        {

            this.Url ??= Microsoft.Maui.Storage.Preferences.Default.Get<string>("url", null!);

            if (this.Url != null)
            {
                this.WebView.Source = new UrlWebViewSource { Url = Url };
                return;
            }

            while (true)
            {

                var url = await this.DisplayPromptAsync("Url", "Enter URL");
                if (url == null)
                {
                    continue;
                }

                this.Url = url;
                Microsoft.Maui.Storage.Preferences.Default.Set("url", url);

                this.WebView.Source = new UrlWebViewSource {  Url = url };
                break;
            }
        }
    }
}
