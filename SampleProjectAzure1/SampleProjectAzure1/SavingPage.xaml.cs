using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleMobileAzure1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavingPage : ContentPage
    {
        private SavingManager _SavingManager = new SavingManager();
        public SavingPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshItems(true);
        }

        private async Task RefreshItems(bool showActivityIndicator)
        {
            using (var scope =  new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
            {
                List<Saving> temp = await _SavingManager.GetSavingAsync2();
                listViewSaving.ItemsSource = await _SavingManager.GetSavingAsync();

                int total = 0;

                for (int i = 0; i < temp.Count; i++)
                {
                    total += temp.ElementAt(i).CashTotal;    
                    System.Diagnostics.Debug.WriteLine(temp.ElementAt(i).Description);
                }
                txtBalance.Text = "Balance = "+total.ToString();
                if (total < 0)
                {
                    contentBalance.BackgroundColor = Color.DarkRed;
                }
                else
                {
                    contentBalance.BackgroundColor = Color.DarkGreen;
                }
                System.Diagnostics.Debug.WriteLine(listViewSaving);
            }
        }

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            TambahSavingPage tambahPage = new TambahSavingPage();

            Saving item = (Saving)e.Item;
            tambahPage.BindingContext = item;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(tambahPage);
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            TambahSavingPage tambahPage = new TambahSavingPage(true);
            await Navigation.PushAsync(tambahPage);
        }

        private async void ListViewSaving_OnRefreshing(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            Exception error = null;
            try
            {
                await RefreshItems(false);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                list.EndRefresh();
            }

            if (error != null)
            {
                await DisplayAlert("Refresh Error !", "Couldn't refresh data (" + error.Message + ")", "OK");
            }
        }
    }
}