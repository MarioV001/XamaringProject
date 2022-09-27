using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace XamaringProject.ViewModels
{
    [QueryProperty(nameof(ItemID), nameof(ItemID))]//used to Query from other forms
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string CName;//custoemr name
        private int PID;//productID
        private decimal PPrice;//product_Price
        public string Id { get; set; }

        public Command RemoveCommand { get; }
        public ItemDetailViewModel()
        {
            RemoveCommand = new Command(RemoveEntry);
        }

        private async void RemoveEntry()
        {
            await DataStore.DeleteItemAsync(Convert.ToInt32(itemId));
            await Shell.Current.GoToAsync("..");//go back to main form
        }

        /// <summary>
        /// form summury
        /// </summary>
        public string CustomerName
        {
            get => CName;
            set => SetProperty(ref CName, value);
        }
        
        public int ProductID
        {
            get => PID;
            set => SetProperty(ref PID, value);
        }
        
        public string ItemID
        {
            get => itemId;
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public decimal ProductPrice
        {
            get => PPrice;
            set => SetProperty(ref PPrice, value);
        }
        
        public async void LoadItemId(string itemId)
        {
           try
           {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.id.ToString();
                CustomerName = item.customerName;
                ProductID = item.productID;
                ProductPrice = item.newPrice;
           }
           catch (Exception)
           {
               Debug.WriteLine("Failed to Load Item");
           }
        }
    }
}
