﻿using AnimaLost2.Service;
using AnimaLost2.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace AnimaLost2.ViewModel
{
    public class ModificationUserViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;
        private IDialogService dialogService;
        private ICommand cancel;
        private ICommand modif;
        private ApplicationUser user;
        private string login;
        private string password;
        private string email;
        private int tel;
        private string userType;

        public string UserType
        {
            get
            {
                if (User != null) userType = SelectedUser.User.RoleName;
                return userType;
            }
            set
            {
                userType = value;
                RaisePropertyChanged("UserType");
            }
        }

        public string Login
        {
            get
            {
                if (User != null) login = SelectedUser.User.UserName;
                return login;
            }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }
        public string Password
        {
            get
            {
                if (User != null && password == null) email = SelectedUser.User.Email;
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
        public string Email
        {
            get
            {
                if (User != null && email == null) email = SelectedUser.User.Email;
                return email;
            }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }
        public int Tel
        {
            get
            {
                if (User != null && tel == 0) tel = SelectedUser.User.Phone;
                return tel;
            }
            set
            {
                tel = value;
                RaisePropertyChanged("Tel");
            }
        }
        public ApplicationUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(() => GoHomeBack());
                }
                return cancel;
            }
        }
        public ICommand Modif
        {
            get
            {
                if (modif == null)
                {
                    modif = new RelayCommand(async () => await ModifUser());
                }
                return modif;
            }
        }

        private async Task ModifUser()
        {
            try
            {
                if (Token.Id == null)
                {
                    navPage.NavigateTo("Login");
                    await dialogService.ShowMessageBox("Acces non autorisé aux utilisateurs", "Session expire");
                }
                else
                {
                    SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/" + Login);
                    if (response.IsSuccessStatusCode)
                    {
                        var userJson = await response.Content.ReadAsStringAsync();
                        var user = ApplicationUser.Deserialize(userJson);
                        if (Password == null) Password = user.Password;
                        if (Email == null) Email = user.Email;
                        if (Tel == 0) Tel = user.Phone;


                        var roleResponse = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/Role/" + user.UserName);
                        var jsonRoleName = await roleResponse.Content.ReadAsStringAsync();
                        user.RoleName = ApplicationUser.GetRoleUser(jsonRoleName);
                        var userFinal = new ApplicationUser()
                        {
                            UserName = user.UserName,
                            Password = Password,
                            Email = Email,
                            Phone = Tel,
                            RoleName = user.RoleName
                        };
                        var responsePut = await SingleConnection.Client.PutAsJsonAsync(SingleConnection.Client.BaseAddress + "Account/" + user.UserName, userFinal);
                        if (responsePut.IsSuccessStatusCode)
                        {
                            await dialogService.ShowMessageBox("La modification s'est bien déroulée", "Modification");
                            GoHomeBack();
                        }
                        else
                        {
                            await dialogService.ShowMessageBox("Il s'est produit une erreur lors de la modification", "Erreur");
                            navPage.NavigateTo("ModificationUser");
                        }
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            await dialogService.ShowMessageBox("L'utilisateur que vous essayé de modifier n'existe pas", "Erreur");
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            await dialogService.ShowMessageBox("Une erreur du serveur est survenue, il se peut que vous ayez été déconnecté", "Erreur");
                            navPage.NavigateTo("Login");
                        }
                        navPage.NavigateTo("ModificationUser");
                    }
                }

            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("La connection au serveur a été perdue", "Erreur connection");
                navPage.NavigateTo("Login");
            }
        }

        public void GoHomeBack()
        {
            navPage.NavigateTo("UserManagement");
        }
        public ModificationUserViewModel(INavigationService lg, IDialogService service)
        {
            navPage = lg;
            dialogService = service;
            User = SelectedUser.User;

        }
    }
}
