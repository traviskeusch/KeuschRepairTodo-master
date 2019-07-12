using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Todo
{
	public partial class TodoListPage : ContentPage
	{
		public TodoListPage()
		{
			InitializeComponent();


            listView.RefreshCommand = new Command(() =>
            {
                // Call OnAppearing to "refresh item source"
                OnAppearing();

                // Turn off refreshing animation
                listView.IsRefreshing = false;
            });
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			// Reset the 'resume' id, since we just want to re-start here
			((App)App.Current).ResumeAtTodoId = -1;

            // Query once to enhance performance and reduce redundant queries
            var dbItems =  await App.Database.GetItemsNotDoneAsync();

            listView.ItemsSource = dbItems;
        }

		async void OnItemAdded(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new TodoItemPage
			{
				BindingContext = new TodoItem()
			});
		}

		async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
            //((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            //Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TodoItemPage
                {
                    BindingContext = e.SelectedItem as TodoItem
                });
            }
		}

        private async void OnDelete(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;
            var item = mi.BindingContext as TodoItem;
            await App.Database.DeleteItemAsync(item);
            OnAppearing();
        }

        private async void OnDoneOrdersClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DoneOrdersPage
            {
                BindingContext = new TodoItemPage()
            });
        }
    }
}
