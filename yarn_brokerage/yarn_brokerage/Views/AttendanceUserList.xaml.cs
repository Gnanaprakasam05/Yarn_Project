using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;
using yarn_brokerage.ViewModels;

namespace yarn_brokerage.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceUserList : ContentPage
    {
        public Approval Approval { get; set; }
        public UserViewModel viewModel { get; set; }
        public AttendanceUserList(Approval approval = null)
        {
            InitializeComponent();

            Approval = approval;

            BindingContext = viewModel = new UserViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
         
            viewModel.LoadUsersCommand.Execute("");

        }
     
        private async void LedgersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as User;
            LedgersListView.SelectedItem = null;
            if (item == null)
                return;
            if (Approval != null)
            {
                Approval.AttendanceTeamNameId = Convert.ToInt32(item.id);
                Approval.AttendanceTeamName = item.user_name;

            }

            await Navigation.PopAsync();
        }

    }
}