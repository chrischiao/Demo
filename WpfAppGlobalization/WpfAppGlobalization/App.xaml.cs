using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace WpfAppGlobalization
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GlobalData.IsDefaultLang = true;
            GlobalData.IsDefaultSkin = true;

            if (e.Args.Length > 0)
                GlobalData.IsDefaultLang = bool.Parse(e.Args[0]);
            if (e.Args.Length > 1)
                GlobalData.IsDefaultSkin = bool.Parse(e.Args[1]);

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(GlobalData.Lang);
            if (!GlobalData.IsDefaultSkin)
                UpdateSkin();
        }

        internal void UpdateSkin()
        {
            try
            {
                var theme = Resources.MergedDictionaries;
                theme.Clear();
                theme.Add(new ResourceDictionary
                {
                    Source = new Uri($"pack://application:,,,/WpfAppGlobalization;component/Resources/Themes/{GlobalData.Skin}.xaml")
                });
                theme.Add(new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/WpfAppGlobalization;component/Resources/Themes/Theme.xaml")
                });

                //var skin = Application.Current.Resources.MergedDictionaries[0];
                //skin.MergedDictionaries.Clear();
                //skin.MergedDictionaries.Add(new ResourceDictionary
                //{
                //    Source = new Uri($"pack://application:,,,/WpfAppGlobalization;component/Resources/Themes/{skinName}.xaml")
                //});

                //var theme = Application.Current.Resources.MergedDictionaries[1];
                //theme.MergedDictionaries.Clear();
                //theme.MergedDictionaries.Add(new ResourceDictionary
                //{
                //    Source = new Uri("pack://application:,,,/WpfAppGlobalization;component/Resources/Themes/Theme.xaml")
                //});

                Current.MainWindow?.OnApplyTemplate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.StackTrace}");
            }
        }
    }
}
