﻿using Xamarin.Forms;

namespace Todo
{
	public class TodoItemPageCS : ContentPage
	{
		public TodoItemPageCS()
		{
			Title = "Todo Item";

			var nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "CustomerName");

            var numberEntry = new Entry();
            numberEntry.SetBinding(Entry.TextProperty, "ContactNumber" );

            var partsCostEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "PartsCost");

            var laborCostEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "LaborCost");

            var totalCostEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "TotalCost");

            var notesEntry = new Entry();
			notesEntry.SetBinding(Entry.TextProperty, "Notes");

			var doneSwitch = new Switch();
			doneSwitch.SetBinding(Switch.IsToggledProperty, "Done");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) =>
			{
				var todoItem = (TodoItem)BindingContext;
				await App.Database.SaveItemAsync(todoItem);
				await Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += async (sender, e) =>
			{
				var todoItem = (TodoItem)BindingContext;
				await App.Database.DeleteItemAsync(todoItem);
				await Navigation.PopAsync();
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += async (sender, e) =>
			{
				await Navigation.PopAsync();
			};

			Content = new StackLayout
			{
				Margin = new Thickness(20),
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children =
				{
					new Label { Text = "Customer Name" },
					nameEntry,
					new Label { Text = "Notes" },
					notesEntry,
                    new Label{Text = "Contact Number"},
                    numberEntry,
                    new Label{Text = "Parts Cost"},
                    partsCostEntry,
                    new Label{Text = "Labor Cost"},
                    laborCostEntry,
                    new Label{Text = "Total Cost"},
                    totalCostEntry,
                    new Label { Text = "Done" },
					doneSwitch,
					saveButton,
					deleteButton,
					cancelButton
				}
			};
		}
	}

    
}
