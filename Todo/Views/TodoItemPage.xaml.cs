using System;
using Xamarin.Forms;

namespace Todo
{
	public partial class TodoItemPage : ContentPage
	{
		public TodoItemPage()
		{
			InitializeComponent();
		}

		async void OnSaveClicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			await App.Database.SaveItemAsync(todoItem);
			await Navigation.PopAsync();
		}

		async void OnDeleteClicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			await App.Database.DeleteItemAsync(todoItem);
			await Navigation.PopAsync();
		}

		async void OnCancelClicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

        private void PartsCostEntry_OnFocused(object sender, FocusEventArgs e)
        {
            PartsCostEntry.Text = "";
        }

        private void LaborCostEntry_OnFocused(object sender, FocusEventArgs e)
        {
            LaborCostEntry.Text = "";
        }

        private void TotalCostEntry_OnFocused(object sender, FocusEventArgs e)
        {
            TotalCostEntry.Text = "";
        }
    }
}
