﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pokens.Battles.Resources {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Pokens.Battles.Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Arena does not exist!.
        /// </summary>
        public static string ArenaNotFound {
            get {
                return ResourceManager.GetString("ArenaNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot challenge yourself!.
        /// </summary>
        public static string CannotChallengeSelf {
            get {
                return ResourceManager.GetString("CannotChallengeSelf", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Required level should be at least 0!.
        /// </summary>
        public static string InvalidArenaLevel {
            get {
                return ResourceManager.GetString("InvalidArenaLevel", resourceCulture);
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
        ///   Looks up a localized string similar to The trainer is invalid!.
        /// </summary>
        public static string InvalidTrainer {
            get {
                return ResourceManager.GetString("InvalidTrainer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You already challenged this trainer!.
        /// </summary>
        public static string TrainerAlreadyChallenged {
            get {
                return ResourceManager.GetString("TrainerAlreadyChallenged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are already enrolled in an arena!.
        /// </summary>
        public static string TrainerAlreadyEnrolled {
            get {
                return ResourceManager.GetString("TrainerAlreadyEnrolled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You do not meet the minimum level requirements!.
        /// </summary>
        public static string TrainerDoesNotMeetMinimumLevel {
            get {
                return ResourceManager.GetString("TrainerDoesNotMeetMinimumLevel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The trainer is not enrolled in any arena!.
        /// </summary>
        public static string TrainerIsNotEnrolled {
            get {
                return ResourceManager.GetString("TrainerIsNotEnrolled", resourceCulture);
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
        
        /// <summary>
        ///   Looks up a localized string similar to The trainers are not enrolled in the same arena!.
        /// </summary>
        public static string TrainersDoNotHaveSameEnrollment {
            get {
                return ResourceManager.GetString("TrainersDoNotHaveSameEnrollment", resourceCulture);
            }
        }
    }
}