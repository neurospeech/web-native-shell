﻿using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using NativeShell.Platforms.iOS.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebKit;
using CoreGraphics;
using NativeShell.Platforms.iOS.Controls;

namespace NativeShell.Controls
{
    partial class NativeWebView
    {
        static partial void OnStaticPlatformInit()
        {
            var config = MauiWKWebView.CreateConfiguration();
            WebViewHandler.PlatformViewFactory =
                handler => handler.VirtualView is NativeWebView
                    ? new NativeWKWebView(CGRect.Empty, (WebViewHandler)handler, config)
                    : new MauiWKWebView(CGRect.Empty, (WebViewHandler)handler, config);
        }

        partial void OnPlatformInit()
        {
            // this.disposables.Register(KeyboardService.Install(this));
        }

        
    }
}
