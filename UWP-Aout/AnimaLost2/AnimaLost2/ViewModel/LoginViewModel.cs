﻿using AnimaLost2.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;


namespace AnimaLost2.ViewModel
{
    public class LoginViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private string uLogin;
        private string password;
        private ICommand singIn;
        private INavigationService navPage;
        private string exception;
        private IDialogService dialogService;

        public LoginViewModel(INavigationService lg, IDialogService dialogService)
        {
            navPage = lg;
            this.dialogService = dialogService;

        }

        public string Exception
        {
            get
            {
                return exception;
            }
            set
            {
                exception = value;
                RaisePropertyChanged("Exception");
            }
        }
        public string ULogin
        {
            get
            {
                return uLogin;
            }
            set
            {
                uLogin = value;
                RaisePropertyChanged("ULogin");
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
        public ICommand SingIn
        {
            get
            {
                if (singIn == null)
                {
                    singIn = new RelayCommand(async () => await Connection());

                }
                return singIn;
            }
        }
        public async Task Connection( )
        {
            var http = new HttpClient();
            var idUser = new IdUser() { UserName = ULogin, Password = Password };

            try
            {
                var stringInput = await http.PostAsJsonAsync("http://smartcityanimal.azurewebsites.net/api/Jwt", idUser);
                if (stringInput.IsSuccessStatusCode)
                {
                    var content2 = await stringInput.Content.ReadAsStringAsync();
                    var tokenSplit = content2.Split('{', '}', ':', ',');
                    Token.Id = tokenSplit[2].TrimEnd('\"').TrimStart('\"');
                    if (Token.Id == null)
                    {
                        navPage.NavigateTo("Login");
                    }
                    else
                    {
                        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var response = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/Admin");
                        if (!response.IsSuccessStatusCode)
                        {
                            await dialogService.ShowMessageBox("Acces non autorisé aux utilisateurs", "Non autorisé");
                        }
                        else
                        {
                            navPage.NavigateTo("UserManagement", ULogin);
                        }    
                    }                 
                }
                else
                {
                    if ((int)stringInput.StatusCode == 400)
                    {
                        await dialogService.ShowMessageBox("Le mot de passe est incorrect", "Error 400");
                    }
                    if ((int)stringInput.StatusCode == 401)
                    {
                        await dialogService.ShowMessageBox("Identifiant invalide", "Error 401");
                    }
                    // je sais plus 
                    if ((int)stringInput.StatusCode == 401)
                    {
                        await dialogService.ShowMessageBox("Identifiant invalide", "Error 401");
                    }

                }
            }
            catch(HttpRequestException e)
            {
                await dialogService.ShowMessageBox("Impossible de se connecter au serveur","Error");
            }
        }
    }

}
