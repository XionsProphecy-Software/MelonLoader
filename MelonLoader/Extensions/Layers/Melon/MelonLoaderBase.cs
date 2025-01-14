﻿using System;

namespace MelonLoader
{
    [Obsolete("MelonLoader.MelonLoaderBase is Only Here for Compatibility Reasons.")]
    public static class MelonLoaderBase
    {
        [Obsolete("MelonLoader.MelonLoaderBase.IsVRChat is Only Here for Compatibility Reasons. Please use MelonLoader.MelonUtils.IsVRChat instead.")]
        public static bool IsVRChat { get => MelonUtils.IsVRChat; }
        [Obsolete("MelonLoader.MelonLoaderBase.IsBoneworks is Only Here for Compatibility Reasons. Please use MelonLoader.MelonUtils.IsBONEWORKS instead.")]
        public static bool IsBoneworks { get => MelonUtils.IsBONEWORKS; }
        [Obsolete("MelonLoader.MelonLoaderBase.UserDataPath is Only Here for Compatibility Reasons. Please use MelonLoader.MelonUtils.GetUserDataDirectory instead.")]
        public static string UserDataPath { get => MelonUtils.UserDataDirectory; }
        [Obsolete("MelonLoader.MelonLoaderBase.UnityVersion is Only Here for Compatibility Reasons. Please use MelonLoader.MelonUtils.GetUnityVersion instead.")]
        public static string UnityVersion { get => string.Copy(MelonUtils.GetUnityVersion()); }
    }
}