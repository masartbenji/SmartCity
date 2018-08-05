using AnimaLost2.Static;
using AnimaLost2.Model;
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
        private IDialogService dialogService;
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
            try
            {
                bool testOK = true;
                if (Login == null || Password == null || Email == null || Tel == 0 || TypeUser == null) { testOK = false; }
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
                        navPage.NavigateTo("Login");
                    }
                    else
                    {
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
        public NewUserViewModel(INavigationService lg, IDialogService service)
        {
            navPage = lg;
            dialogService = service;
        }
    }
}
