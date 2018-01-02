using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NavApp1.Model;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace NavApp1.ViewModel
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string uLogin;
        private string password;
        private ICommand singIn;
        private INavigationService navPage;

        public string ULogin {
            get {
                return uLogin;
            }
            set{
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
        public ICommand SingIn {

            get {
                if (singIn == null) {
                    singIn = new RelayCommand(async () => await Connection());
                }
                return singIn;
            }
        }
        public async Task Connection()
        {
            /*    
                    Token token;
                    var http = new HttpClient();
                    string json = JsonConvert.SerializeObject(Login);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    try
                    {
                        var stringInput = await http.PostAsync(new Uri(CoursierApi.URL_BASE + CoursierApi.URL_JWT), content);
                        var content2 = await stringInput.Content.ReadAsStringAsync();
                        token = JsonConvert.DeserializeObject<Token>(content2);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.Write(e);
                        token = null;
                    }
                if (token==null) {
                    navPage.NavigateTo("UserManagement", Login);
                }   **/
            navPage.NavigateTo("UserManagement",ULogin);
        }
        public LoginViewModel(INavigationService lg)
        {
            navPage = lg;
        }
        public void GoBackHome()
        {
            navPage.NavigateTo("Login");
        }
    }
}
