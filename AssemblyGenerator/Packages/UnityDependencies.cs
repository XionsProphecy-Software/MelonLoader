﻿using System.IO;

namespace MelonLoader.AssemblyGenerator
{
    internal class UnityDependencies : PackageBase
    {
        internal UnityDependencies()
        {
            Version = Utils.GetUnityVersion();
            URL = "https://github.com/HerpDerpinstine/MelonLoader/raw/master/BaseLibs/UnityDependencies/" + Version + ".zip";
            Destination = Path.Combine(Main.BasePath, "UnityDependencies");
        }

        private void Save()
        {
            Config.UnityVersion = Version;
            Config.Save();
        }

        private bool ShouldDownload() => (string.IsNullOrEmpty(Config.UnityVersion) || !Config.UnityVersion.Equals(Version));

        internal override bool Download()
        {
            if (!ShouldDownload())
            {
                Logger.Msg("Unity Dependencies are up to date. No Download Needed.");
                return true;
            }
            Logger.Msg("Downloading Unity " + Version + " Dependencies...");
            if (base.Download())
            {
                Save();
                return true;
            }
            return false;
        }
    }
}