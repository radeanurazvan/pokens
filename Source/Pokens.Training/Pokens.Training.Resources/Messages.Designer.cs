﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pokens.Training.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Pokens.Training.Resources.Messages", typeof(Messages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You didn&apos;t succeed into catching this Pokemon. Try again later!.
        /// </summary>
        public static string CatchFailed {
            get {
                return ResourceManager.GetString("CatchFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The id is invalid!.
        /// </summary>
        public static string InvalidId {
            get {
                return ResourceManager.GetString("InvalidId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name is invalid!.
        /// </summary>
        public static string InvalidName {
            get {
                return ResourceManager.GetString("InvalidName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Pokemon definition!.
        /// </summary>
        public static string InvalidPokemonDefinition {
            get {
                return ResourceManager.GetString("InvalidPokemonDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stats should not be null!.
        /// </summary>
        public static string NullStats {
            get {
                return ResourceManager.GetString("NullStats", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pokemon not found!.
        /// </summary>
        public static string PokemonNotFound {
            get {
                return ResourceManager.GetString("PokemonNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This Pokemon is not for starters!.
        /// </summary>
        public static string PokemonNotStarter {
            get {
                return ResourceManager.GetString("PokemonNotStarter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You already have a starter Pokemon!.
        /// </summary>
        public static string TrainerAlreadyHasStarter {
            get {
                return ResourceManager.GetString("TrainerAlreadyHasStarter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You already have this Pokemon!.
        /// </summary>
        public static string TrainerAlreadyHasThisPokemon {
            get {
                return ResourceManager.GetString("TrainerAlreadyHasThisPokemon", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Trainer does not exist!.
        /// </summary>
        public static string TrainerNotFound {
            get {
                return ResourceManager.GetString("TrainerNotFound", resourceCulture);
            }
        }
    }
}
