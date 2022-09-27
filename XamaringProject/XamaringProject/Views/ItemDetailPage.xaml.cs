using System.ComponentModel;
using Xamarin.Forms;
using XamaringProject.ViewModels;

namespace XamaringProject.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}