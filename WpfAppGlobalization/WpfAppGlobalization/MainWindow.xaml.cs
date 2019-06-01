using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppGlobalization.Properties.Langs;
using System.Diagnostics;
using System.Reflection;

namespace WpfAppGlobalization
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSL_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Lang.HintMsg);
            GlobalData.SwitchLang();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Lang.Cancel);
            this.Close();
        }

        private void ButtonSS_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.SwitchSkin();
        }
    }
}
