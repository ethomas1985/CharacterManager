﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test.Serializers.PSRD.TestData {
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
    internal class TestResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TestResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Test.Serializers.PSRD.TestData.TestResources", typeof(TestResources).Assembly);
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
        ///   Looks up a localized string similar to &lt;h1&gt;Check&lt;/h1&gt;
        ///&lt;p&gt;This is the first paragraph of the Check Section&lt;/p&gt;
        ///&lt;h1&gt;First Sub Header&lt;/h1&gt;
        ///&lt;p&gt;Second Paragraph&lt;/p&gt;
        ///&lt;h1&gt;&lt;/h1&gt;
        ///&lt;p&gt;Third Paragraph&lt;/p&gt;
        ///&lt;h1&gt;Second Subheader&lt;/h1&gt;
        ///&lt;p&gt;Fourth Paragraph&lt;/p&gt;
        ///&lt;h1&gt;Third Subheader&lt;/h1&gt;
        ///&lt;p&gt;Fifth Paragraph&lt;/p&gt;.
        /// </summary>
        internal static string Expected_Checks {
            get {
                return ResourceManager.GetString("Expected_Checks", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///    &quot;type&quot;: &quot;skill&quot;, 
        ///    &quot;name&quot;: &quot;Test Skills/Acrobatics&quot;, 
        ///    &quot;attribute&quot;: &quot;Dex&quot;, 
        ///    &quot;source&quot;: &quot;Core Rulebook&quot;, 
        ///    &quot;trained_only&quot;: true, 
        ///    &quot;armor_check_penalty&quot;: true, 
        ///    &quot;description&quot;: &quot;This is the Description Field&quot;,
        ///    &quot;sections&quot;: [
        ///        {
        ///            &quot;name&quot;: &quot;Check&quot;, 
        ///            &quot;source&quot;: &quot;Core Rulebook&quot;,
        ///            &quot;body&quot;: &quot;&lt;p&gt;This is the first paragraph of the Check Section&lt;/p&gt;&quot;, 
        ///            &quot;type&quot;: &quot;section&quot;, 
        ///            &quot;sections&quot;: [
        ///                {
        ///           [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string TestSkill {
            get {
                return ResourceManager.GetString("TestSkill", resourceCulture);
            }
        }
    }
}
