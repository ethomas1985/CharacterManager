﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test.Serializers.Json {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Test.Serializers.Json.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to {
        ///	&quot;Age&quot;: 0,
        ///	&quot;Alignment&quot;: 0,
        ///	&quot;Deity&quot;: 0,
        ///	&quot;Gender&quot;: 0,
        ///	&quot;Eyes&quot;: null,
        ///	&quot;Hair&quot;: null,
        ///	&quot;Height&quot;: 0,
        ///	&quot;Weight&quot;: 0,
        ///	&quot;Homeland&quot;: null,
        ///	&quot;Name&quot;: null,
        ///	&quot;Race&quot;: null,
        ///	&quot;BaseSize&quot;: 0,
        ///	&quot;Size&quot;: 0,
        ///	&quot;Languages&quot;: null,
        ///	&quot;MaxHealthPoints&quot;: 0,
        ///	&quot;Damage&quot;: 0,
        ///	&quot;HealthPoints&quot;: 0,
        ///	&quot;BaseSpeed&quot;: 0,
        ///	&quot;ArmoredSpeed&quot;: 0,
        ///	&quot;BaseAttackBonus&quot;: 0,
        ///	&quot;BaseFortitude&quot;: 0,
        ///	&quot;BaseReflex&quot;: 0,
        ///	&quot;BaseWill&quot;: 0,
        ///	&quot;Strength&quot;: {
        ///		&quot;Type&quot;: &quot;Strength&quot;,
        ///		&quot;Score&quot;: 12,
        ///		&quot;Modifier&quot;: 1,
        ///		&quot;Base&quot;: 12,
        ///		&quot;Enhanced&quot;: 0,
        ///		&quot;Inherent&quot;: 0,
        ///		&quot;Penalty&quot; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TestCharacter {
            get {
                return ResourceManager.GetString("TestCharacter", resourceCulture);
            }
        }
    }
}
