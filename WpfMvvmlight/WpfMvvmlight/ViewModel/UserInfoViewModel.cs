using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WpfMvvmlight.Model;

namespace WpfMvvmlight.ViewModel
{
    public class UserInfoViewModel : ViewModelBase
    {

        public UserInfoViewModel()
        {
            UserInfo = new UserInfoModel();
     
            ShowCommand = new RelayCommand(() => { Messenger.Default.Send<string>("", "SHOW"); });
            UpdateCommand = new RelayCommand(() => { Messenger.Default.Send<string>(UserInfo.Name, "UPDATE"); });
            CloseCommand = new RelayCommand(() => { Messenger.Default.Send<string>("", "CLOSE"); });
        }

        private UserInfoModel _userInfo;
        public UserInfoModel UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; RaisePropertyChanged(() => UserInfo); }
        }

        public RelayCommand ShowCommand { get; private set; }

        public RelayCommand UpdateCommand { get; private set; }

        public RelayCommand CloseCommand { get; private set; }
    }
}
