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
using LOLQuerier.ViewModels;

namespace LOLQuerier.Views
{
    /// <summary>
    /// QueryView.xaml 的交互逻辑
    /// </summary>
    public partial class QueryView : Window
    {
        QueryViewModel viewModel;

        public QueryView()
        {
            viewModel = new QueryViewModel();
            InitializeComponent();

            this.DataContext = viewModel;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.Region))
                return;

            if (string.IsNullOrEmpty(viewModel.SummonerName))
                return;

#if false
            if (!string.Equals(viewModel.SummonerName, "N00b lol player"))
                viewModel.SummonerName = "usukhuu";
#endif

            var summonerProfile = viewModel.Query();
            if (summonerProfile != null)
            {
                ProfileView profileView = new ProfileView(summonerProfile);
                profileView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Not Found");
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
