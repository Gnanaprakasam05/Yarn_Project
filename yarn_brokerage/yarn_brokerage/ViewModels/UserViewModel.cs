using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

using yarn_brokerage.Models;
using yarn_brokerage.Views;

namespace yarn_brokerage.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public User _User { get; set; }
        public Command LoadUsersCommand { get; set; }

        public UserViewModel()
        {
            Title = "User List";
            Users = new ObservableCollection<User>();

            LoadUsersCommand = new Command(async (object SearchString) => await ExecuteLoadUsersCommand(SearchString));

        }

        async Task ExecuteLoadUsersCommand(object SearchString)
        {
            string _SearchString = "";
            if (SearchString != null)
                _SearchString = (string)SearchString;

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Users.Clear();
                try
                {
                    var current = Connectivity.NetworkAccess;

                    if (current == NetworkAccess.Internet)
                    {
                        using (var cl = new HttpClient())
                        {
                            HttpContent formcontent = null;

                            var request = await cl.PostAsync(Application.Current.Properties["HOSTURL"] + "api/user_list", formcontent);

                            //request.EnsureSuccessStatusCode(); 

                            var response = await request.Content.ReadAsStringAsync();

                            User_Data res = Newtonsoft.Json.JsonConvert.DeserializeObject<User_Data>(response);
                            foreach (User item in res.user)
                            {
                                Users.Add(item);
                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}