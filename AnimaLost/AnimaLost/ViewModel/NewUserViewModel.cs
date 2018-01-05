using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using AnimaLost.Model;

namespace AnimaLost.ViewModel
{
    public class NewUserViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;
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
        }// pas sur de ca 
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

            //garnir le user et le type de user 
            // envoie a la base de donnes

            using (var http = new HttpClient())
            {
                //a verif sur le case obligatoire sont remplis
                bool testOK = true;
                if (Login == null) { testOK = false; }
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
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    var response = await http.PostAsJsonAsync("http://smartcityanimal.azurewebsites.net/api/Account", newUser);
                    if (response.IsSuccessStatusCode)
                    {
                        GoHomeBack();
                    }
                    else if (response.ReasonPhrase == "Unauthorized")
                    {
                        navPage.NavigateTo("Login");
                    }
                    else
                    {
                        navPage.NavigateTo("NewUser");
                    }
                }
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
        public NewUserViewModel(INavigationService lg)
        {
            navPage = lg;
        }
    }
}
