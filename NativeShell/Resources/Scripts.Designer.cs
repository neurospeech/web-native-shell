﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NativeShell.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Scripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Scripts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NativeShell.Resources.Scripts", typeof(Scripts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (function () {
        ///    let id = 1;
        ///
        ///    const map = new Map();
        ///
        ///    function runCode($rid$, $code$, $args$) {
        ///        return function () {
        ///            try {
        ///                let a = $args$;
        ///                let result = ($code$).apply({ clr, evalInPage }, a);
        ///                if (result &amp;&amp; result.then) {
        ///                    result.then((r) =&gt; {
        ///                        evalInPage(`window.nativeShell.on($rid$, ${serialize(r) || 1})`);
        ///                    }, (e) =&gt; {
        ///                        evalInPage(` [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string NativeShell {
            get {
                return ResourceManager.GetString("NativeShell", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to function __awaiter(thisArg, _arguments, P, generator) {
        ///    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
        ///    return new (P || (P = Promise))(function (resolve, reject) {
        ///        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        ///        function rejected(value) { try { step(generator[&quot;throw&quot;](value)); } catch (e) { reject(e); } }
        ///        function step(result) { result.done ? resolve(result.value) :  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TSLib {
            get {
                return ResourceManager.GetString("TSLib", resourceCulture);
            }
        }
    }
}
