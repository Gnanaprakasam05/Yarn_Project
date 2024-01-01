using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PopupGroup : PopupPage
    {
        DraftConfirmationViewModel viewModel;
        public ObservableCollection<MessageGroup> MessageGroup_Check { get; set; }
        public MessageGroup MessageGroup_List { get; set; }
        public DraftConfirmation DraftConfirmation { get; set; }
        public int Message_Group;
        public PopupGroup(DraftConfirmation draftConfirmation = null)
        {
            InitializeComponent();
            MessageGroup_Check = new ObservableCollection<MessageGroup>();
            BindingContext = viewModel = new DraftConfirmationViewModel();
            DraftConfirmation = draftConfirmation;
            MessageGroup_List = new MessageGroup();
        }

        public MessageGroup PreviousSelectedTeam { get; set; }
        private MessageGroup _selectedTeam { get; set; }
        public MessageGroup SelectedTeam
        {
            get { return _selectedTeam; }
            set
            {
                if (_selectedTeam != value)
                {
                    _selectedTeam = value;
                    ExpandOrCollapseSelectedItem();
                }
            }
        }
        private void ExpandOrCollapseSelectedItem()
        {
            if (PreviousSelectedTeam != null)
            {
                MessageGroup_Check.Where(t => t.Id == PreviousSelectedTeam.Id).FirstOrDefault().BackgroundColor = System.Drawing.Color.Transparent;
            }

            MessageGroup_Check.Where(t => t.Id == SelectedTeam.Id).FirstOrDefault().BackgroundColor = System.Drawing.Color.LightGray;

            PreviousSelectedTeam = SelectedTeam;
        }



        ViewCell lastCell;
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            MessageGroup_Check = await viewModel.MessageGroupDraftConfirmationCommand();

            var selectedItem = MessageGroup_Check.FirstOrDefault(item => item.Id == DraftConfirmation.SupplierWhatsappGroup_Ckeck); //DraftConfirmation.SupplierWhatsappGroup_Ckeck
            SelectedTeam = selectedItem;
            MessageGroup_List.TeamGroupId = SelectedTeam.TeamGroupId;

        }

        private async void messageGrouplist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedTeam = e.SelectedItem as MessageGroup;
            MessageGroup_List.TeamGroupId = SelectedTeam.TeamGroupId;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (MessageGroup_List.TeamGroupId != 0)
            {
                bool valueCheck = await viewModel.WhatsappConfirmationDraftConfirmationCommand(DraftConfirmation, MessageGroup_List);
                if (valueCheck == true)
                {
                    DraftConfirmation.Send_Back_Check = true;
                    await PopupNavigation.PopAsync(true);

                }
            }
        }
    }
}