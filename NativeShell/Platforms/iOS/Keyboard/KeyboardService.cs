using Foundation;
using NativeShell.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using WebKit;

namespace NativeShell.Platforms.iOS.Keyboard
{
    class KeyboardService
    {

        static double UpdateHeightMargin(
            WKWebView iOSWebView,
            NativeWebView webView,
            System.Drawing.RectangleF rect) {
            var height = rect.Height;
            iOSWebView.LayoutMargins = new UIEdgeInsets(0,0, height, 0);
            return height;
        }
        public static IDisposable Install(WKWebView iOSWebView, NativeWebView webView)
        {


            var defaultCenter = NSNotificationCenter.DefaultCenter;
            var didShow = defaultCenter.AddObserver(UIKeyboard.DidShowNotification, (n) => {
                if (n.UserInfo == null)
                {
                    return;
                }
                NSValue result = (NSValue)n.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
                var height = UpdateHeightMargin(iOSWebView, webView, result.RectangleFValue);
                try
                {
                    webView.Eval($"document.body.dataset.keyboard = 'shown'; document.body.dataset.keyboardHeight = {height};");
                }
                catch { }
            });
            var didChange = defaultCenter.AddObserver(UIKeyboard.DidChangeFrameNotification, (n) => {
                if (n.UserInfo == null)
                {
                    return;
                }
                NSValue result = (NSValue)n.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
                var height = UpdateHeightMargin(iOSWebView, webView, result.RectangleFValue);
                try
                {
                    webView.Eval(
                        $"document.body.dataset.keyboard = 'shown'; document.body.dataset.keyboardHeight = {height};");
                }
                catch { }
            });
            var didHide = defaultCenter.AddObserver(UIKeyboard.DidHideNotification, (n) => {
                var height = UpdateHeightMargin(iOSWebView, webView, new System.Drawing.RectangleF(0,0,0,0));
                try
                {
                    webView.Eval("document.body.dataset.keyboard = 'hidden'; document.body.dataset.keyboardHeight = 0;");
                }
                catch { }
            });

            return new DisposableAction(delegate {
                defaultCenter.RemoveObserver(didShow);
                defaultCenter.RemoveObserver(didChange);
                defaultCenter.RemoveObserver(didHide);
            });
        }

    }
}
