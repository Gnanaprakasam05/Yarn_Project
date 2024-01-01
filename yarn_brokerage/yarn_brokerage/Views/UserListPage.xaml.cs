using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using yarn_brokerage.Models;
using yarn_brokerage.Views;
using yarn_brokerage.ViewModels;

namespace yarn_brokerage.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserListPage : ContentPage
	{
        UserViewModel viewModel;
        public User User { get; set; }
        public SearchFilter SearchFilter { get; set; }
        public SearchConfirmationFilter SearchConfirmationFilter { get; set; }
        public SearchApprovalFilter SearchApprovalFilter { get; set; }
        public SearchDispatchConfirmationFilter SearchDispatchConfirmationFilter { get; set; }
        public SearchCallLogFilter SearchCallLogFilter { get; set; }
        
        public UserListPage(User user=null, SearchFilter searchFilter = null,SearchConfirmationFilter searchConfirmationFilter=null, SearchApprovalFilter searchApprovalFilter = null, SearchDispatchConfirmationFilter searchDispatchConfirmationFilter= null,SearchCallLogFilter searchCallLogFilter= null)
		{
			InitializeComponent ();
            User = user;
            SearchFilter = searchFilter;
            SearchConfirmationFilter = searchConfirmationFilter;
            SearchCallLogFilter = searchCallLogFilter;
            SearchApprovalFilter = searchApprovalFilter;
            SearchDispatchConfirmationFilter = searchDispatchConfirmationFilter;
            BindingContext = viewModel = new UserViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as User;
            UserListView.SelectedItem = null;
            if (item == null)
                return;
            if (User != null)
            {
                User.id = item.id;
                User.user_name = item.user_name;
            }
            if (SearchFilter != null)
            {
                SearchFilter.user_id = item.id;
                SearchFilter.user_name = item.user_name;
            }
            if (SearchConfirmationFilter != null)
            {
                SearchConfirmationFilter.user_id = item.id;
                SearchConfirmationFilter.user_name = item.user_name;
            }
            if (SearchApprovalFilter != null)
            {
                SearchApprovalFilter.user_id = item.id;
                SearchApprovalFilter.user_name = item.user_name;
            }
            if (SearchDispatchConfirmationFilter != null)
            {
                SearchDispatchConfirmationFilter.user_id = item.id;
                SearchDispatchConfirmationFilter.user_name = item.user_name;
            }
            if (SearchCallLogFilter != null)
            {
                SearchCallLogFilter.user_id = item.id;
                SearchCallLogFilter.user_name = item.user_name;
            }
            await Navigation.PopModalAsync();            
        }
        //async void AddUser_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new AddUserPage(null,1)));
        //}
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.User.User == 0)
                viewModel.LoadUsersCommand.Execute(null);
        }

        //private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    viewModel.LoadUsersCommand.Execute(txtSearch.Text);
        //}
    }
}