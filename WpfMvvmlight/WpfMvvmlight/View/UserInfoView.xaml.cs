﻿using System;
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
using GalaSoft.MvvmLight.Messaging;

namespace WpfMvvmlight.View
{
    /// <summary>
    /// UserInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoView : Window
    {
        public UserInfoView()
        {
            InitializeComponent();

            Messenger.Default.Register<string>(this, "CLOSE", str => { this.Close(); });

            Messenger.Default.Register<string>(this, "SHOW", str => 
            {
                var welcomeView = new WelcomeView();
                welcomeView.Show();
            });
        }
    }
}
