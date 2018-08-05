﻿using AnimaLost2.ViewModel;
<<<<<<< HEAD
using AnimaLost2.Static;
=======
using AnimaLost2.Connection;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost2
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
            SimpleIoc.Default.Register<GestionAnnonceViewModel>();
            SimpleIoc.Default.Register<SingleConnection>();

            NavigationService navigationPages = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationPages);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            navigationPages.Configure("GestionAnnonce", typeof(GestionAnnonce));
            navigationPages.Configure("ModificationUser", typeof(ModificationUser));
            navigationPages.Configure("Login", typeof(Login));
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
            get
            {
                return ServiceLocator.Current.GetInstance<UserManagementViewModel>();
            }

        }
        public NewUserViewModel NewUser
        {
            get
            {
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
        public GestionAnnonceViewModel GestionAnnonce
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GestionAnnonceViewModel>();
            }
        }
    }
}