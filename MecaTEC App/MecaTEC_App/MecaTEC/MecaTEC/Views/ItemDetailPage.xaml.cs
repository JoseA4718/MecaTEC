using MecaTEC.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MecaTEC.Views
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