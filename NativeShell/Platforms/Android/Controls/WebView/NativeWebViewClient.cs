using Android.Graphics;
using Android.Webkit;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using NativeShell.Controls;
using NativeShell.Keyboard;
using NativeShell.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Views.ViewGroup;

namespace NativeShell.Platforms.Android.Controls
{
    class NativeWebViewClient : MauiWebViewClient
    {
        private readonly global::Android.Webkit.WebView platformView;
        private readonly NativeWebView nativeWebView;

        public NativeWebViewClient(WebViewHandler handler, NativeWebView nativeWebView) : base(handler)
        {

            this.platformView = handler.PlatformView;
            this.nativeWebView = nativeWebView;

           
        }

        public override void OnPageStarted(global::Android.Webkit.WebView? view, string? url, Bitmap? favicon)
        {
            base.OnPageStarted(view, url, favicon);
            this.nativeWebView.Eval(Scripts.NativeShell);
            KeyboardService.Instance.Refresh();
        }

        public override void OnPageFinished(global::Android.Webkit.WebView? view, string? url)
        {
            base.OnPageFinished(view, url);
            this.nativeWebView.IsPageReady = true;
            KeyboardService.Instance.Refresh();
        }


        class MessageCallback: WebMessagePort.WebMessageCallback
        {
            private readonly NativeWebView client;
            private readonly WebMessagePort sender;

            public MessageCallback(NativeWebView client, WebMessagePort sender)
            {
                this.client = client;
                this.sender = sender;
            }

            public override void OnMessage(WebMessagePort? port, WebMessage? message)
            {
                client.RunMainThreadJavaScript(message.Data);
            }
        }
    }
}
