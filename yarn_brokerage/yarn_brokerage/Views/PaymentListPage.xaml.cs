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
	public partial class PaymentListPage : ContentPage
	{
        PaymentViewModel viewModel;
        
        public DraftConfirmationPayment DraftConfirmationPayment { get; set; }
        public DraftConfirmationDispatchDelivery DraftConfirmationDispatchDelivery { get; set; }
        public PaymentListPage (DraftConfirmationDispatchDelivery _draftConfirmationDispatchDelivery, DraftConfirmationPayment draftConfirmationPayment)
		{
			InitializeComponent ();

            DraftConfirmationDispatchDelivery = _draftConfirmationDispatchDelivery;
            DraftConfirmationPayment = draftConfirmationPayment;
            BindingContext = viewModel = new PaymentViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as DraftConfirmationPayment;
            PaymentListView.SelectedItem = null;
            if (item == null)
                return;
            if (DraftConfirmationPayment != null)
            {
                DraftConfirmationPayment.from_advance_id = item.id;
                DraftConfirmationPayment.amount = item.excess_amount - item.utilized_amount;
                DraftConfirmationPayment.utr_number = item.payment_date.ToString("dd-MM-yyyy") + " / " + item.utr_number;
            }
            await Navigation.PopAsync();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Payment.Payment == 0)
                viewModel.LoadItemsCommand.Execute(DraftConfirmationDispatchDelivery);
        }

    }
}