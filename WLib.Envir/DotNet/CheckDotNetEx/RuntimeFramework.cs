/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Win32;

namespace WLib.Envir.DotNet.CheckDotNetEx
{
    /// <summary>
    /// See http://nunit.org, this class from NUnit Project's RuntimeFramework.cs
    /// RuntimeFramework represents a particular version of a common language runtime implementation.
    /// </summary>
    [Serializable]
    public sealed class RuntimeFramework
    {
        public RuntimeType Runtime { get; private set; }
        public Version Version { get; private set; }
        public string DisplayName { get; private set; }
        static RuntimeFramework currentFramework;

        /// <summary>
        /// 
        /// </summary>
        public static RuntimeFramework CurrentFramework
        {
            get
            {
                if (currentFramework == null)
                {
                    var monoRuntimeType = Type.GetType("Mono.Runtime", false);
                    var runtime = monoRuntimeType != null ? RuntimeType.Mono : RuntimeType.Net;
                    currentFramework = new RuntimeFramework(runtime, System.Environment.Version);
                    if (monoRuntimeType != null)
                    {
                        var method = monoRuntimeType.GetMethod("GetDisplayName", BindingFlags.Static |
                                                                                 BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.ExactBinding);
                        if (method != null) currentFramework.DisplayName = (string)method.Invoke(null, new object[0]);
                    }
                }
                return currentFramework;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static RuntimeFramework[] AvailableFrameworks
        {
            get
            {
                var frameworks = new List<RuntimeFramework>();
                foreach (var framework in GetAvailableFrameworks(RuntimeType.Net)) frameworks.Add(framework);
                foreach (var framework in GetAvailableFrameworks(RuntimeType.Mono)) frameworks.Add(framework);
                return frameworks.ToArray();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsMonoInstalled()
        {
            if (CurrentFramework.Runtime == RuntimeType.Mono) return true;
            // Don't know how to do this on linux yet, but it's not a problem since we are only supporting Mono on Linux
            if (System.Environment.OSVersion.Platform != PlatformID.Win32NT) return false;
            var key = Registry.LocalMachine.OpenSubKey(@"Software\Novell\Mono");
            if (key == null) return false;
            var version = key.GetValue("DefaultCLR") as string;
            if (string.IsNullOrEmpty(version)) return false;
            return key.OpenSubKey(version) != null;
        }
        /// <summary>
        /// Returns an array of all available frameworks of a given type, for example, all mono or all .NET frameworks.
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        public static RuntimeFramework[] GetAvailableFrameworks(RuntimeType rt)
        {
            var frameworks = new List<RuntimeFramework>();
            if (rt == RuntimeType.Net && System.Environment.OSVersion.Platform != PlatformID.Unix)
            {
                var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\.NETFramework\policy");
                if (key != null)
                    foreach (var name in key.GetSubKeyNames())
                        if (name.StartsWith("v"))
                            foreach (var build in key.OpenSubKey(name).GetValueNames())
                                frameworks.Add(new RuntimeFramework(rt, new Version(name.Substring(1) + "." + build)));
            }
            else if (rt == RuntimeType.Mono && IsMonoInstalled())
            {
                var framework = new RuntimeFramework(rt, new Version(1, 1, 4322));
                framework.DisplayName = "Mono 1.0 Profile";
                frameworks.Add(framework);
                framework = new RuntimeFramework(rt, new Version(2, 0, 50727));
                framework.DisplayName = "Mono 2.0 Profile";
                frameworks.Add(framework);
            }
            return frameworks.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtime"></param>
        /// <param name="version"></param>
        public RuntimeFramework(RuntimeType runtime, Version version)
        {
            Runtime = runtime;
            Version = version;
            DisplayName = runtime.ToString() + " " + version.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayName;
        }
    }
}