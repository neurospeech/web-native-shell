using CoreGraphics;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using NativeShell.Controls;
using NativeShell.Platforms.iOS.Controls.WebView;
using NativeShell.Resources;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebKit;

namespace NativeShell.Platforms.iOS.Controls
{
    internal class MainScriptInvoker : NSObject, IWKScriptMessageHandlerWithReply
    {
        private readonly Action<string, Action<string>> messageAction;

        public MainScriptInvoker(Action<string,Action<string>> MessageAction)
        {
            messageAction = MessageAction;
        }

        public void DidReceiveScriptMessage(
            WKUserContentController userContentController, 
            WKScriptMessage message, 
            Action<NSObject, NSString> replyHandler)
        {
            messageAction(message.Body.ToString(), (msg) => replyHandler((NSString)msg, null!));
        }
    }

    class NativeWebViewUserContentController: WebKit.WKUserContentController {


        public NativeWebViewUserContentController()
        {
            var script = Scripts.NativeShell;
            this.AddUserScript(new WKUserScript((NSString)script, WebKit.WKUserScriptInjectionTime.AtDocumentStart, false));

        }

        
    }

    internal class NativeWKWebView : MauiWKWebView
    {
        private static WKWebViewConfiguration Init(WKWebViewConfiguration configuration)
        {
            configuration.AllowsInlineMediaPlayback = true;
            configuration.Preferences.JavaScriptCanOpenWindowsAutomatically = true;
            configuration.Preferences.JavaScriptEnabled = true;
            configuration.MediaTypesRequiringUserActionForPlayback = WKAudiovisualMediaTypes.None;
            configuration.UserContentController = new NativeWebViewUserContentController();
            return configuration;
        }

        public NativeWKWebView(
            CGRect frame,
            WebViewHandler handler,
            WKWebViewConfiguration configuration) : base(frame, handler, Init(configuration))
        {
            if (handler.VirtualView is NativeWebView nativeWebView)
            {

                this.UIDelegate = new NativeWebViewUIDelegate(handler);
                // this.NavigationDelegate = new NativeWebViewNavigationDelegate(this.NavigationDelegate);

                if (configuration.UserContentController is NativeWebViewUserContentController nwc)
                {
                    nwc.AddScriptMessageHandler(new MainScriptInvoker((s, a) =>
                    {
                        nativeWebView.RunMainThreadJavaScript(s);
                        a("queued");
                    }), WKContentWorld.Page, "mainScript");

                }
            }
            
            ScrollView.Bounces = false;
            AutosizesSubviews = true;
            ClipsToBounds = true;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            ScrollView.Frame = Bounds;
        }
    }
}
