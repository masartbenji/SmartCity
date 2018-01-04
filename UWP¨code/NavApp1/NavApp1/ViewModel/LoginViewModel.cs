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
        public async Task Connection()
        {
            var http = new HttpClient();
            var idUser = new IdUser() { UserName = ULogin, Password = Password };
            try
            {
                var stringInput = await http.PostAsJsonAsync("http://localhost:55945/api/Jwt", idUser);
                if(stringInput.ReasonPhrase != "Unauthorized")
                {
                    var content2 = await stringInput.Content.ReadAsStringAsync();
                    var tokenSplit = content2.Split('{', '}', ':', ',');
                    Token.Id = tokenSplit[2].TrimEnd('\"').TrimStart('\"');
                }
            }
            catch (HttpRequestException e)
            {
                Token.Id = null;
            }
            if (Token.Id == null)
            {
                GoBackHome();
            }
            else
            {
                navPage.NavigateTo("UserManagement", ULogin);
            }
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
    class IdUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
