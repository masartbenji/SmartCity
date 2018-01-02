using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NavApp1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace NavApp1.ViewModel
{
    public class NewUserViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private INavigationService navPage;
        private ICommand cancel;
        private ICommand creation;
        private string login;
        private string password;
        private string nom;
        private string prenom;
        private string email;
        private int tel; //Maybe need to change de type of the box to only allowed numbers
        private int codePostal;//same as tel
        private object typeUser;

        public object TypeUser
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
        public int CodePostal
        {
            get
            {
                return codePostal;
            }
            set
            {
                codePostal = value;
                RaisePropertyChanged("CodePostal");
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
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
                RaisePropertyChanged("Nom");
            }
        }
        public string Prenom
        {
            get
            {
                return prenom;
            }
            set
            {
                prenom = value;
                RaisePropertyChanged("Prenom");
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
        public ICommand Creation {
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
            //a verif sur le case obligatoire sont remplis
            bool testOK = true;
            if (login == null){testOK = false;}
            if( password == null) { testOK = false;}
            if(nom == null) { testOK = false; }
            if(prenom == null) { testOK = false; }

            if (!testOK)
            {
                ApplicationUser newUser = new ApplicationUser();
                //garnir le user et le type de user 
                // envoie a la base de donnes

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
