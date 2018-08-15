using AnimaLost2.Service;
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
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

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
        private string tel;
        private string _typeUserSelected;
        public string TypeUserSelected
        {
            get
            {
                return _typeUserSelected;
            }
            set
            {
                _typeUserSelected = value;
                if (_typeUserSelected != null)
                {
                    RaisePropertyChanged("TypeUserSelected");
                }


            }

        }
        private ObservableCollection<string> _typeUserList;
        public ObservableCollection<string> TypeUserList
        {
            get
            {

                return _typeUserList;
            }
            set
            {
                if (_typeUserList == value)
                {
                    return;
                }

                _typeUserList = value;
                RaisePropertyChanged("TypeUserList");
            }

        }

        public string Tel
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
                string typeUserBD;
                bool testOK = true;
                if (Login == null || Password == null || TypeUserSelected == null || Email == null || Tel == null) {
                    testOK = false;
                    await dialogService.ShowMessageBox("Vous devez remplir correctement tous les champs.", "Erreur");
                }
                if (testOK)
                {
                    testOK = IsPhoneAllowed(Tel);
                    if (!testOK)
                    {
                        await dialogService.ShowMessageBox("Numéro de téléphone invalide", "Téléphone");
                    }
                }
                if (testOK)
                {
                    testOK = IsEmailAllowed(Email);
                    if (!testOK)
                    {
                        await dialogService.ShowMessageBox("Email invalide", "Email");
                    }
                }

                if (TypeUserSelected == "Admin") typeUserBD = "Admin"; else typeUserBD = "User";
                if (testOK)
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = Login,
                        Password = Password,
                        Email = Email,
                        Phone = int.Parse(Tel),
                        RoleName = typeUserBD
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
                        await dialogService.ShowMessageBox("Votre session a expiré", "Erreur");
                        navPage.NavigateTo("Login");
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        await dialogService.ShowMessageBox("Le mot de passe n'a pas été entré correctement, celui ci doit contenir 8 caractères, une majuscule, un caractère spécial et au moins un chiffre", "Erreur");
                    }
                    else
                    {
                        await dialogService.ShowMessageBox("L'utilisateur ne peut être créé", "Erreur");
                        navPage.NavigateTo("NewUser");
                    }
                }
            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("La connection au serveur a été perdue", "Erreur");
                navPage.NavigateTo("Login");
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
            initListTypeUser();
        }
        public bool IsEmailAllowed(string text)
        {
            bool blnValidEmail = false;
            Regex regEMail = new Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (text.Length > 0)
            {
                blnValidEmail = regEMail.IsMatch(text);
            }

            return blnValidEmail;
        }
        public bool IsPhoneAllowed(string text)
        {
            bool blnValidPhone = false;
            Regex regPhone = new Regex(@"^[0-9]*$");
            if (text.Length > 0)
            {
                blnValidPhone = regPhone.IsMatch(text);
            }

            return blnValidPhone;
        }
        public void initListTypeUser()
        {
            // on peut faire une requtte pour avoire la liste de la bd des =/= roles 
            _typeUserList = new ObservableCollection<string>();
            _typeUserList.Add("Utilisateur");
            _typeUserList.Add("Admin");
        }
        
}
}
