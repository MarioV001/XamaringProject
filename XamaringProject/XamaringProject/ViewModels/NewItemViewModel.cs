using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamaringProject.Models;

namespace XamaringProject.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string customerName;
        private string productid;
        private string newPriceInput;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(customerName)
                && !String.IsNullOrWhiteSpace(productid)
                && !String.IsNullOrWhiteSpace(newPriceInput);
        }

        public string Name //used on the forms binding
        {
            get => customerName;
            set => SetProperty(ref customerName, value);
        }

        public string ProductID //used on the forms binding
        {
            get => productid;
            set => SetProperty(ref productid, value);
        }

        public string NewPriceInput //used on the forms binding
        {
            get => newPriceInput;
            set => SetProperty(ref newPriceInput, value);
        }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                id = 0,//the controller doesnt care about the new ID
                customerName = Name,
                productID = Convert.ToInt32(ProductID),
                newPrice = Convert.ToDecimal(NewPriceInput)
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
