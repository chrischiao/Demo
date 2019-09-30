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
using System.Windows.Shapes;

namespace LOLQuerier.Views
{
    /// <summary>
    /// ProfileView.xaml 的交互逻辑
    /// </summary>
    public partial class ProfileView : Window
    {
        public ProfileView(object viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            QueryView queryView = new QueryView();
            queryView.Show();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
