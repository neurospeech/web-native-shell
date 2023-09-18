using NativeShell.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeShell.Controls
{

    public partial class NativeWebView : WebView
    {

        static NativeWebView() {
            OnStaticPlatformInit();
        }

        private DisposableList disposables = new DisposableList();

        public IJSContext Context { get;set; }

        partial void OnPlatformInit();

        static  partial void OnStaticPlatformInit();

        public GlobalClr Clr { get; }

        public NativeWebView()
        {
            Context = JSContextFactory.Instance.Create();
            this.Clr = new GlobalClr();
            Context["clr"] = Context.Marshal(Clr);

            Context["evalInPage"] = Context.CreateFunction(1, (c, s) => {
                this.Eval(s.ToString());
                return Context.Undefined;
            }, "sendToBrowser");

            this.VerticalOptions = LayoutOptions.Fill;
            this.HorizontalOptions = LayoutOptions.Fill;

            // setup channel...
            OnPlatformInit();

            
        }

        /// <summary>
        /// This JavaScript will have access to entire CLR and will be able to execute
        /// everything in CLR.
        /// </summary>
        /// <param name="script"></param>
        /// <param name="callback"></param>
        public void RunMainThreadJavaScript(string script)
        {
            Dispatcher.DispatchTask( async () => {
                try
                {
                    var result = await this.Clr.SerializeAsync(Context.Evaluate(script));
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            });
        }

        ~NativeWebView()
        {
            disposables.Dispose();
        }


    }
}
