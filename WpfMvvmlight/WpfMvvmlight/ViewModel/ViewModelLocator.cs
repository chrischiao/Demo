using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;

namespace WpfMvvmlight.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<UserInfoViewModel>();
            SimpleIoc.Default.Register<WelcomeViewModel>();
        }

        public UserInfoViewModel UserInfo
        {
            get { return ServiceLocator.Current.GetInstance<UserInfoViewModel>(); }
        }

        public WelcomeViewModel WelcomeVM
        {
            get { return ServiceLocator.Current.GetInstance<WelcomeViewModel>(); }
        }

        public static void Cleanup()
        {
            // TODO clear the ViewModels
        }
    }
}
