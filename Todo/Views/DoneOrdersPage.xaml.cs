using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Todo
{
    public partial class DoneOrdersPage : ContentPage
    {
        public DoneOrdersPage()
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
            //base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = await App.Database.GetItemsDoneAsync();
        }


        void ReturnToItemList(object sender, EventArgs e)
        {
            Navigation.PopAsync();
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

    }
}