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

        public static IDisposable Install(NativeWebView webView)
        {
            var defaultCenter = NSNotificationCenter.DefaultCenter;
            var didShow = defaultCenter.AddObserver(UIKeyboard.DidShowNotification, (n) => {
                if (n.UserInfo == null)
                {
                    return;
                }
                NSValue result = (NSValue)n.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
                var rect = result.RectangleFValue;
                var height = rect.Height / (rect.Height + rect.Top);
                webView.Margin = new Thickness(0, 0, 0, webView.Height * height);
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
                var rect = result.RectangleFValue;
                var height = rect.Height / (rect.Height + rect.Top);
                webView.Margin = new Thickness(0, 0, 0, webView.Height * height);
                try
                {
                    webView.Eval(
                        $"document.body.dataset.keyboard = 'shown'; document.body.dataset.keyboardHeight = {height};");
                }
                catch { }
            });
            var didHide = defaultCenter.AddObserver(UIKeyboard.DidHideNotification, (n) => {
                webView.Margin = new Thickness(0);
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
