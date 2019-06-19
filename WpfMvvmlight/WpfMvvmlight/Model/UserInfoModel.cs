using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace WpfMvvmlight.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoModel : ObservableObject
    {
        private string _name;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        private string _phone;
        /// <summary>
        /// 用户电话
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; RaisePropertyChanged(() => Phone); }
        }

        private int _sex;
        /// <summary>
        /// 用户性别
        /// </summary>
        public int Sex
        {
            get { return _sex; }
            set { _sex = value; RaisePropertyChanged(() => Sex); }
        }

        private string _address;
        /// <summary>
        /// 用户地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; RaisePropertyChanged(() => Address); }
        }
    }
}
