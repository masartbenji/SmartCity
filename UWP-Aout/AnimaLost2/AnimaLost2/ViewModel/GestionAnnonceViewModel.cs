<<<<<<< HEAD
﻿using AnimaLost2.Model;
using AnimaLost2.Static;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
=======
﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.ComponentModel;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace AnimaLost2.ViewModel
{
    public class GestionAnnonceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;
        private ICommand goBackHome;
        private string userID;
        private string emailUser;
<<<<<<< HEAD
        private string phone;
        private int nbAnnouncement;
        private ObservableCollection<Announcement> announcements;
        public ObservableCollection<Announcement> Announcements
        {
            get
            {
                return announcements;
            }
            set
            {
                announcements = value;
                RaisePropertyChanged("Announcements");
            }
        }

        public GestionAnnonceViewModel(INavigationService lg)
        {
            InitializeAsync();
            navPage = lg;
        }
        private async void InitializeAsync()
        {
            Announcements = await GetAnnouncementsUser();
        }
=======

        public GestionAnnonceViewModel(INavigationService lg)
        {
            navPage = lg;
        }
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        public void OnNavigatedTo(NavigationEventArgs e)
        {

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
<<<<<<< HEAD
                userID = SelectedUser.User.UserName;
=======
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
                emailUser = SelectedUser.User.Email;
=======
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
                return emailUser;
            }
            set
            {
                emailUser = value;
                RaisePropertyChanged("EmailUser");
            }
        }
<<<<<<< HEAD
        public string PhoneUser
        {
            get
            {
                phone = "0" + SelectedUser.User.Phone.ToString() ;
                return phone;
            }
            set
            {
                phone = value;
                RaisePropertyChanged("PhoneUser");
            }
        }
        public int NbAnnonceUser
        {
            get
            {
                return nbAnnouncement;
            }
            set
            {
                nbAnnouncement = value;
                RaisePropertyChanged("NbAnnonceUser");
            }
        }
=======
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        private ICommand refreshList;
        public ICommand RefreshList
        {
            get
            {
                if (refreshList == null)
                {
                    refreshList = new RelayCommand(() => Refresh());
                }
                return refreshList;
            }
        }
        public void Refresh()
        {
            // a voir si il faut pas clear du coup la liste dans l initialize
        }
        public void Home()
        {
            navPage.NavigateTo("UserManagement");
        }
<<<<<<< HEAD
        public async Task<ObservableCollection<Announcement>> GetAnnouncementsUser()
        {
            ObservableCollection<Announcement> announcements = new ObservableCollection<Announcement>();
            try
            {
                SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/Announcement/" + SelectedUser.User.UserName);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAnnouncement = response.Content.ReadAsStringAsync().Result;
                    Announcements = Announcement.Deserialize(jsonAnnouncement);

                }
            }
            catch (HttpRequestException)
            {

            }
            return Announcements;
        }
=======
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
    }
}
