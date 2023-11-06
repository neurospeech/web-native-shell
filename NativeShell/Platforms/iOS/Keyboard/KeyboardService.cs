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



            // var inset = new UIEdgeInsets(0,0, height, 0);
            // iOSWebView.ScrollView.ContentInset = new UIEdgeInsets(0, 0, iOSWebView.ScrollView.VisibleSize.Height - height, 0);
            // iOSWebView.ScrollView.ContentMode = UIViewContentMode.ScaleToFill;
            // iOSWebView.ScrollView.ContentOffset = new CoreGraphics.CGPoint()
            // iOSWebView.Bounds = new CoreGraphics.CGRect(0,0,  iOSWebView.InvalidateIntrinsicContentSize())
            // iOSWebView.InvalidateIntrinsicContentSize();
            // iOSWebView.LayoutIfNeeded();
            // iOSWebView.ScrollView.ContentInset = new UIEdgeInsets(0, 0, height, 0);

            // iOSWebView.SizeToFit();
            // iOSWebView.SetNeedsLayout();

            // iOSWebView.SetViewportInsets(inset, UIEdgeInsets.Zero);

            // iOSWebView.ScrollView.Bounds = new CoreGraphics.CGRect(0, 0, iOSWebView.Bounds.Width, iOSWebView.Bounds.Height - height);
            // iOSWebView.ScrollView.LayoutIfNeeded();

            var parent = iOSWebView.Superview;
            var parentBounds = parent.Bounds;

            // iOSWebView.LayoutMargins = new UIEdgeInsets(0, 0, -height, 0);
            // iOSWebView.LayoutIfNeeded();
            // iOSWebView.SizeToFit();

            iOSWebView.Bounds = new CoreGraphics.CGRect(parentBounds.Left, parentBounds.Top, parentBounds.Width, parentBounds.Height - height);
            iOSWebView.SizeToFit();
            // iOSWebView.RemoveConstraints(iOSWebView.Constraints);
            // iOSWebView.AddConstraint(NSLayoutConstraint.Create( );
            
            
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
