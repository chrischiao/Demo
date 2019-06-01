using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppGlobalization
{
    public class GlobalData
    {
        private const string cn = "zh-CN";
        private const string en = "en-US";
        private const string DefaultSkin = "DefaultSkin";
        private const string DarkSkin = "DarkSkin";

        public static string Lang => IsDefaultLang ? cn : en;

        public static string Skin => IsDefaultSkin ? DefaultSkin : DarkSkin;

        public static bool IsDefaultSkin { get; set; }

        public static bool IsDefaultLang { get; set; }

        #region TODO : 读写配置文件

        public static void SwitchLang()
        {
            IsDefaultLang = !IsDefaultLang;
            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location, $"{IsDefaultLang} {IsDefaultSkin}");
            Environment.Exit(0);
        }

        public static void SwitchSkin()
        {
            IsDefaultSkin = !IsDefaultSkin;
            ((App)System.Windows.Application.Current).UpdateSkin();
        }

        #endregion
    }

    internal class AppConfig
    {
        public string Lang { get; set; }

        public string Skin { get; set; }
    }
}
