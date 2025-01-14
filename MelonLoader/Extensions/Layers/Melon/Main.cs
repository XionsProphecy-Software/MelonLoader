﻿using System;
using System.Collections.Generic;

namespace MelonLoader
{
    [Obsolete("MelonLoader.Main is Only Here for Compatibility Reasons.")]
    public static class Main
    {
        [Obsolete("MelonLoader.Main.Mods is Only Here for Compatibility Reasons. Please use MelonLoader.MelonHandler.Mods instead.")]
        public static List<MelonMod> Mods = null;
        [Obsolete("MelonLoader.Main.Plugins is Only Here for Compatibility Reasons. Please use MelonLoader.MelonHandler.Plugins instead.")]
        public static List<MelonPlugin> Plugins = null;
        [Obsolete("MelonLoader.Main.IsVRChat is Only Here for Compatibility Reasons. Please use MelonLoader.MelonUtils.IsVRChat instead.")]
        public static bool IsVRChat = false;
        [Obsolete("MelonLoader.Main.IsBoneworks is Only Here for Compatibility Reasons. Please use MelonLoader.MelonUtils.IsBONEWORKS instead.")]
        public static bool IsBoneworks = false;
        [Obsolete("MelonLoader.Main.GetUnityVersion is Only Here for Compatibility Reasons. Please use MelonLoader.MelonUtils.GetUnityVersion instead.")]
        public static string GetUnityVersion() => string.Copy(MelonUtils.GetUnityVersion());
        [Obsolete("MelonLoader.Main.GetUserDataPath is Only Here for Compatibility Reasons. Please use MelonLoader.MelonUtils.GetUserDataDirectory instead.")]
        public static string GetUserDataPath() => MelonUtils.UserDataDirectory;
    }
}