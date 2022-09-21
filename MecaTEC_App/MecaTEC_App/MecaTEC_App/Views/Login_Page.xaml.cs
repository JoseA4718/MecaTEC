using MecaTEC_App.REST_API_LoginModel;
using MecaTEC_App.REST_API_UserModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MecaTEC_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class allows users to login in Cook Time application, if already have an account created.
    /// @author Jose A.
    /// </summary>
    public partial class Login_Page : ContentPage

    {
        public static string ip = "192.168.0.100";
        /// <summary>
        /// This constructor execute Login Page partial class
        /// @author Jose A.
        /// </summary>
        public Login_Page()
        {
            InitializeComponent();
        }


        /// <summary>
        /// This method is used to change the current page to Register page
        /// @author Mauricio C.
        /// </summary>
        private void Go_Register(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.Register_Page());
        }

        /// <summary>
        /// This method is used to change the current page to Home page and send a json file to verify the user credentials
        /// @author Mauricio C.
        /// </summary>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            var userValidate = userEntry.Text;
            if (!string.IsNullOrEmpty(userValidate))
            {

                string email = userEntry.Text;
                string password = "/" + CreateMD5(pasEntry.Text);
                HttpClient cliente = new HttpClient();
                string url = "http://" + ip + ":6969/login/" + email + password;
                var result = await cliente.GetAsync(url);
                var json = result.Content.ReadAsStringAsync().Result;
                MecaTEC_App.REST_API_UserModel.User InputUser = new REST_API_UserModel.User();
                InputUser = MecaTEC_App.REST_API_UserModel.User.FromJson(json);
                if (InputUser.Email == null)
                {
                    await DisplayAlert("MecaTEC", "The data you filled with does not match with any CookTime user. Please verify your info and try again!", "OK");
                }
                else
                {
                    MecaTEC_App.REST_API_UserModel.User CURRENTUSER = MecaTEC_App.REST_API_UserModel.User.FromJson(json);
                    await DisplayAlert("Cook Time", "Welcome back " + CURRENTUSER.FirstName, "OK");
                    await Navigation.PushAsync(new Home_Page());
                }
            }

        }
        /// <summary>
        /// This method is used to change the current page to Info page
        /// @author Mauricio C.
        /// </summary>
        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Info_Page());
        }
        /// <summary>
        /// This method encrypt the password with HASH MD5 algorithm
        /// @author Mauricio C.
        /// </summary>
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}