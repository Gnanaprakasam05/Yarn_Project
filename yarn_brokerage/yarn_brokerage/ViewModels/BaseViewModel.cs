using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using yarn_brokerage.Models;
using yarn_brokerage.Services;

namespace yarn_brokerage.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string no_of_count_with_bag_weight = string.Empty;
        public string No_Of_Count_With_Bag_Weight
        {
            get { return no_of_count_with_bag_weight; }
            set { SetProperty(ref no_of_count_with_bag_weight, value); }
        }


        string no_of_pending_today_with_bag_weight = string.Empty;
        public string No_Of_Pending_Today_With_Bag_Weight
        {
            get { return no_of_pending_today_with_bag_weight; }
            set { SetProperty(ref no_of_pending_today_with_bag_weight, value); }
        }
        string no_of_pending_delay_with_bag_weight = string.Empty;
        public string No_Of_Pending_Delay_With_Bag_Weight
        {
            get { return no_of_pending_delay_with_bag_weight; }
            set { SetProperty(ref no_of_pending_delay_with_bag_weight, value); }
        }

        string no_of_pending_future_with_bag_weight = string.Empty;
        public string No_Of_Pending_Future_With_Bag_Weight
        {
            get { return no_of_pending_future_with_bag_weight; }
            set { SetProperty(ref no_of_pending_future_with_bag_weight, value); }
        }









        string balance_value = string.Empty;
        public string Balance_Value
        {
            get { return balance_value; }
            set { SetProperty(ref balance_value, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
