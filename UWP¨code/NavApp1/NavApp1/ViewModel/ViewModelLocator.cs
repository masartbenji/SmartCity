﻿using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using NavApp1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavApp1.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<UserManagementViewModel>();
            SimpleIoc.Default.Register<NewUserViewModel>();
            SimpleIoc.Default.Register<ModificationUserViewModel>();

            NavigationService navigationPages = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationPages);

            navigationPages.Configure("ModificationUser", typeof(ModificationUser));
            navigationPages.Configure("Login", typeof(Login));//login dans xaml xCLAssee ATTEntion 
            navigationPages.Configure("UserManagement", typeof(UserManagement));
            navigationPages.Configure("NewUser", typeof(NewUser));
        }
        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }
        public UserManagementViewModel UserManagement
        {
            get {
                return ServiceLocator.Current.GetInstance<UserManagementViewModel>();
            }

        }
        public NewUserViewModel NewUser
        {
            get {
                return ServiceLocator.Current.GetInstance<NewUserViewModel>();
            }
        }
        public ModificationUserViewModel ModificationUser
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ModificationUserViewModel>();
            }
        }
    }
}
