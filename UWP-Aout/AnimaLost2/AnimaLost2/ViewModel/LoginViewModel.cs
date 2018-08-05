<<<<<<< HEAD
﻿using AnimaLost2.Static;
using AnimaLost2.Model;
=======
﻿using AnimaLost2.Model;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
=======
            var http = new HttpClient();
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
            var idUser = new IdUser() { UserName = ULogin, Password = Password };

            try
            {
<<<<<<< HEAD
                var stringInput = await SingleConnection.Client.PostAsJsonAsync(SingleConnection.Client.BaseAddress + "Jwt", idUser);
=======
                var stringInput = await http.PostAsJsonAsync("http://smartcityanimal.azurewebsites.net/api/Jwt", idUser);
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
                        SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/Role/" + idUser.UserName);
                        string roleName = await response.Content.ReadAsStringAsync();
                        if (ApplicationUser.GetRoleUser(roleName) != "Admin") 
=======
                        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var response = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/Admin");
                        if (!response.IsSuccessStatusCode)
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
=======
                    // je sais plus 
                    if ((int)stringInput.StatusCode == 401)
                    {
                        await dialogService.ShowMessageBox("Identifiant invalide", "Error 401");
                    }

>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
                }
            }
            catch(HttpRequestException e)
            {
                await dialogService.ShowMessageBox("Impossible de se connecter au serveur","Error");
            }
        }
    }
<<<<<<< HEAD
    class IdUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
=======
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc

}
