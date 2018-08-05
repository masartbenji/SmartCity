<<<<<<< HEAD
﻿using AnimaLost2.Static;
using AnimaLost2.Model;
=======
﻿using AnimaLost2.Model;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnimaLost2.ViewModel
{
    public class NewUserViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;
<<<<<<< HEAD
        private IDialogService dialogService;
=======
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        private ICommand cancel;
        private ICommand creation;
        private string login;
        private string password;
        private string email;
        private int tel; //Maybe need to change de type of the box to only allowed numbers
        private string typeUser;

        public string TypeUser
        {
            get
            {
                return typeUser;
            }
            set
            {
                typeUser = value;
                RaisePropertyChanged("TypeUser");
            }
        }
        public int Tel
        {
            get
            {
                return tel;
            }
            set
            {
                tel = value;
                RaisePropertyChanged("Tel");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
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
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }

        }
        public ICommand Creation
        {
            get
            {
                if (creation == null)
                {
                    creation = new RelayCommand(async () => await AjoutNouveau());
                }
                return creation;
            }
        }

        public async Task AjoutNouveau()
        {
<<<<<<< HEAD
            try
            {
                bool testOK = true;
                if (Login == null || Password == null || Email == null || Tel == 0 || TypeUser == null) { testOK = false; }
=======
            using (var http = new HttpClient())
            {
                //a verif sur le case obligatoire sont remplis
                bool testOK = true;
                if (Login == null) { testOK = false; }
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
                if (Password == null) { testOK = false; }
                if (testOK)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = Login,
                        Password = Password,
                        Email = Email,
                        Phone = Tel,
                        RoleName = TypeUser
                    };
<<<<<<< HEAD
                    SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    var response = await SingleConnection.Client.PostAsJsonAsync(SingleConnection.Client.BaseAddress + "Account", newUser);
                    if (response.IsSuccessStatusCode)
                    {
                        await dialogService.ShowMessageBox("L'utilisateur a bien été créé", "Création");
                        navPage.NavigateTo("UserManagement");
                    }
                    else if (response.ReasonPhrase == "Unauthorized")
                    {
                        await dialogService.ShowMessageBox("Vous n'êtes pas autorisé à effectuer cette action", "Erreur");
=======
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    var response = await http.PostAsJsonAsync("http://smartcityanimal.azurewebsites.net/api/Account", newUser);
                    if (response.IsSuccessStatusCode)
                    {
                        GoHomeBack();
                    }
                    else if (response.ReasonPhrase == "Unauthorized")
                    {
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
                        navPage.NavigateTo("Login");
                    }
                    else
                    {
<<<<<<< HEAD
                        await dialogService.ShowMessageBox("L'utilisateur ne peut être créé", "Erreur");
                        navPage.NavigateTo("NewUser");
                    }
                }
                else
                {
                    await dialogService.ShowMessageBox("Vous devez remplir tous les champs", "Erreur");
                    navPage.NavigateTo("UserManagement");
                }
            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("la connection au serveur a été perdue", "Erreur");
            }
=======
                        navPage.NavigateTo("NewUser");
                    }


                }
                else
                {
                    // SI PAS OK msg errorr

                }
            }
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
        public void GoHomeBack()
        {
            navPage.NavigateTo("UserManagement");
        }
<<<<<<< HEAD
        public NewUserViewModel(INavigationService lg, IDialogService service)
        {
            navPage = lg;
            dialogService = service;
=======
        public NewUserViewModel(INavigationService lg)
        {
            navPage = lg;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        }
    }
}
