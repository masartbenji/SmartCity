using AnimaLost2.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class GestionAnnonceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;
        private ICommand goBackHome;
        private ICommand suppression;
        private string userID;
        private string emailUser;
        private int nbAnnonceUser;
        private int phoneUser;
        private ObservableCollection<AnnouncementVisuel> announcementVisuel1;
        public ObservableCollection<AnnouncementVisuel> AnnouncementVisuel1
        {
            get
            {
                return announcementVisuel1;
            }
            set
            {
                if (announcementVisuel1 != null)
                {
                    return;
                }
                announcementVisuel1 = value;
                RaisePropertyChanged("AnnouncementVisuel1");
            }
        }
        private AnnouncementVisuel selectAnnounce;

        public GestionAnnonceViewModel(INavigationService lg)
        {
            navPage = lg;
            AnnouncementVisuel1 = new ObservableCollection<AnnouncementVisuel>();
            init();
        }
        public void init()
        {
            AnnouncementVisuel1.Add(new AnnouncementVisuel
            {
                idAnnoun = 789,
                DateAnnoun = new DateTime(2017, 12, 10),
                NameAnimal = "LuoLou",
                Breed = "ezffze",
                Species = "zarzar",
                Description = "JE sais pas ",
            });
            AnnouncementVisuel1.Add(new AnnouncementVisuel
            {
                idAnnoun = 789,
                DateAnnoun = new DateTime(2017, 12, 10),
                NameAnimal = "LuoLou",
                Breed = "ezffze",
                Species = "zarzar",
                Description = "JE sais pas ",
            });
        }

        //public async void InitializeAsync()
        //{

        //    using(HttpClient http = new HttpClient())
        //    {
        //        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
        //        var responseUser = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/" + UserID);
        //        var userJson = await responseUser.Content.ReadAsStringAsync();
        //        var user = ApplicationUser.Deserialize(userJson);
        //        EmailUser = user.Email;
        //        PhoneUser = user.Phone;
        //        NbAnnonceUser = nbAnnonce(user);

        //        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
        //        var response = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/Announcement/" + UserID);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseAnnouncement = await response.Content.ReadAsStringAsync();
        //            var listAnnounc = responseAnnouncement.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);

        //            foreach(string announcement in listAnnounc)
        //            {
        //                //AnnouncementVisuel announcementVisu = AnnouncementVisuel.Deserialise(announcement);
        //                //Announcement.Add(announcementVisu);
        //            }
        //        }
        //    }
        //}
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
       

        public AnnouncementVisuel SelectAnnounce
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
                    suppression = new RelayCommand(() => DeleteAnnouncement(SelectAnnounce.idAnnoun));
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
            navPage.NavigateTo("GestionAnnonce");
        }
        
        

    }
}
