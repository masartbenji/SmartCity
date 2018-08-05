<<<<<<< HEAD
﻿using AnimaLost2.Static;
using AnimaLost2.Model;
=======
﻿using AnimaLost2.Model;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
        private IDialogService dialogService;
        private ICommand cancel;
        private ICommand modif;
=======
        private ICommand cancel;
        private ICommand modif;
        private ICommand supp;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        private string login;
        private string password;
        private string email;
        private int tel;

        public string Login
        {
<<<<<<< HEAD
            get {
                if (SelectedUser.User != null && login == null) login = SelectedUser.User.UserName;
                return login;
            }
=======
            get { return login; }
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("password");
            }
        }
        public string Email
        {
<<<<<<< HEAD
            get {
                if (SelectedUser.User != null && email == null) email = SelectedUser.User.Email;
                return email;
            }
=======
            get { return email; }
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }
        public int Tel
        {
<<<<<<< HEAD
            get {
                if (SelectedUser.User != null && tel == 0) tel = SelectedUser.User.Phone;
                return tel;
            }
=======
            get { return tel; }
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
            set
            {
                tel = value;
                RaisePropertyChanged("Tel");
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
<<<<<<< HEAD

        private async Task ModifUser()
        {
            try
            {
                SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/" + Login);
=======
        public ICommand Supp
        {
            get
            {
                if(supp == null)
                {
                    supp = new RelayCommand(async () => await SuppUser());
                }
                return supp;
            }
        }

        private async Task ModifUser()
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/" + Login);
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync();
                    var user = ApplicationUser.Deserialize(userJson);
<<<<<<< HEAD
                    if (Password == "") Password = user.Password;
                    if (Email == "") Email = user.Email;
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
                    else if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        await dialogService.ShowMessageBox("Une erreur du serveur est survenue, il se peut que vous ayez été déconnecté", "Erreur");
                    }
                    navPage.NavigateTo("ModificationUser");
                }
            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("La connection au serveur a été perdue", "Erreur");
            }

=======
                    if (user == null) navPage.NavigateTo("ModificationUser");
                    else
                    {
                        if (Password == "") Password = user.Password;
                        if (Email == "") Email = user.Email;
                        if (Tel == 0) Tel = user.Phone;
                        var roleResponse = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/Role" + user.UserName);
                        user.RoleName = await roleResponse.Content.ReadAsStringAsync();
                        ApplicationUser userFinal = new ApplicationUser()
                        {
                            UserName = user.UserName,
                            Password = Password,
                            Phone = Tel,
                            Email = Email,
                            RoleName = user.RoleName
                        };
                        var responsePut = await http.PutAsJsonAsync("http://smartcityanimal.azurewebsites.net/api/Account", userFinal);
                        if (responsePut.IsSuccessStatusCode)
                        {
                            GoHomeBack();
                        }
                        else
                        {
                            navPage.NavigateTo("ModificationUser");
                        }
                    }
                }
            }
        }
        private async Task SuppUser()
        {
            using(HttpClient http = new HttpClient())
            {
                if(Login != "")
                {
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    var response = await http.DeleteAsync("http://smartcityanimal.azurewebsites.net/api/Account" + Login);
                    var envoi = response.RequestMessage;
                    if (response.IsSuccessStatusCode)
                    {
                        GoHomeBack();
                    }
                    else navPage.NavigateTo("ModificationUser");
                }
            }
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        }

        public void GoHomeBack()
        {
            navPage.NavigateTo("UserManagement");
        }
<<<<<<< HEAD
        public ModificationUserViewModel(INavigationService lg, IDialogService service)
        {
            navPage = lg;
            dialogService = service;
=======
        public ModificationUserViewModel(INavigationService lg)
        {
            navPage = lg;
        }
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            Login = e.Parameter.ToString(); 
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        }
    }
}
