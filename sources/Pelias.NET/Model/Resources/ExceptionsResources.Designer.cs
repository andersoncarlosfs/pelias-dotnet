﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pelias.NET.Model.Resources {
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
    internal class ExceptionsResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionsResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Pelias.NET.Model.Resources.ExceptionsResources", typeof(ExceptionsResources).Assembly);
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
        ///   Looks up a localized string similar to The collection &apos;{(0)}&apos; has &apos;{(1)}&apos; elements instead of &apos;{(2)}&apos;..
        /// </summary>
        internal static string CollectionIterationException {
            get {
                return ResourceManager.GetString("CollectionIterationException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The request to &apos;{(0)}&apos; with the parameters &apos;{(1)}&apos; returned a response from &apos;{(2)}&apos; with a status code &apos;{(3)}&apos; and the message from the servers is &apos;{(4)}&apos;..
        /// </summary>
        internal static string HttpRequestException {
            get {
                return ResourceManager.GetString("HttpRequestException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The entry &apos;{(0)}&apos; in &apos;{(1)}&apos; is not supported..
        /// </summary>
        internal static string MissingEntryException {
            get {
                return ResourceManager.GetString("MissingEntryException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The reader &apos;{(0)}&apos; read a token type of &apos;{(1)}&apos; and it is not equal to the type &apos;{(2)}&apos; of the argument &apos;{(3)}&apos;..
        /// </summary>
        internal static string TypeMismatchException_NotEqual_JsonTokenType {
            get {
                return ResourceManager.GetString("TypeMismatchException_NotEqual_JsonTokenType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type &apos;{(0)}&apos; of the argument &apos;{(1)}&apos; is not the equal to the type &apos;{(2)}&apos; of the argument &apos;{(3)}&apos;..
        /// </summary>
        internal static string TypeMismatchException_NotEqual_JsonValueKind {
            get {
                return ResourceManager.GetString("TypeMismatchException_NotEqual_JsonValueKind", resourceCulture);
            }
        }
    }
}
