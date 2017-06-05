using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleMobileAzure1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TambahSavingPage : ContentPage
    {
        private SavingManager _SavingManager = new SavingManager();
        private bool _isNew = false;
        public TambahSavingPage()
        {
            InitializeComponent();
        }

        private void ClearAll()
        {
            foreach (var ctr in gvSaving.Children)
            {
                if (ctr is Entry)
                {
                    var item = ctr as Entry;
                    item.Text = string.Empty;
                }
            }
        }

        public TambahSavingPage(bool isNew)
        {
            InitializeComponent();
            _isNew = isNew;
            if (_isNew)
            {
                txtDescription.Focus();
            }
        }

        private async void BtnSave_OnClicked(object sender, EventArgs e)
        {
            if (_isNew)
            {
                var Saving = new Saving()
                {
                    Description = txtDescription.Text,
                    CashTotal = Convert.ToInt32(txtCashTotal.Text)
                };
                await _SavingManager.SaveTaskAsync(Saving);
                ClearAll();
                await DisplayAlert("Keterangan", "Data Saving berhasil ditambah !", "OK");
            }
            else
            {
                var Saving = (Saving)this.BindingContext;
                await _SavingManager.SaveTaskAsync(Saving);
                await DisplayAlert("Keterangan", "Data Saving berhasil diupdate !", "OK");
            }
        }
    }
}
