using AnimaLost2.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace AnimaLost2.ViewModel
{
    public class GestionAnnonceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;
        private ICommand goBackHome;
        private ICommand suppression;
        private string userID;
        private string emailUser;
        private int nbAnnonceUser;
        private int phoneUser;
        private ObservableCollection<Announcement> announcement;
        private Announcement selectAnnounce;

        public GestionAnnonceViewModel(INavigationService lg)
        {
            navPage = lg;
            Announcement = new ObservableCollection<Announcement>();
            InitializeAsync();
        }
        public void InitializeAsync()
        {
            //init

        }


        public void OnNavigatedTo(NavigationEventArgs e)
        {
            ApplicationUser user = (ApplicationUser)e.Parameter;
            UserID = user.UserName;
            EmailUser = user.Email;
            PhoneUser = user.Phone;
            NbAnnonceUser = nbAnnonce(user);
        }
        public int nbAnnonce(ApplicationUser user)
        {
           // REcherche le nb annonce;
            return 1;
        }
        private ObservableCollection<Announcement> Announcement
        {
            get
            {
                return announcement;
            }
            set
            {
                if (announcement != null)
                {
                    return;
                }
                announcement = value;
                RaisePropertyChanged("Announcement");
            }
        }
        public Announcement SelectAnnounce
        {
            get
            {
                return selectAnnounce;
            }
            set
            {
                selectAnnounce = value;
                if (selectAnnounce != null)
                {
                    RaisePropertyChanged("SelectAnnounce");
                }
            }
        }

        public ICommand Suppression
        {
            get
            {
                if (suppression == null)
                {
                    suppression = new RelayCommand(() => DeleteAnnouncement(SelectAnnounce.Id));
                }
                return suppression;

            }


        }
        public void DeleteAnnouncement(int id)
        {
            // effacer la liste et faire un refresh 
        }

        public ICommand GoBackHome
        {
            get
            {
                if (goBackHome == null)
                {
                    goBackHome = new RelayCommand(() => Home());
                }
                return goBackHome;
            }

        }
        public string UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
                RaisePropertyChanged("UserID");
            }
        }
        public string EmailUser
        {
            get
            {
                return emailUser;
            }
            set
            {
                emailUser = value;
                RaisePropertyChanged("EmailUser");
            }
        }
        public int NbAnnonceUser
        {
            get
            {
                return nbAnnonceUser;
            }
            set
            {
                nbAnnonceUser = value;
                RaisePropertyChanged("NbAnnonceUser");
            }
        }
        public int PhoneUser
        {
            get
            {
                return phoneUser;
            }
            set
            {
                phoneUser = value;
                RaisePropertyChanged("PhoneUser");
            }
        }

        public void Home()
        {
            navPage.NavigateTo("UserManagement");
        }







    }
}
