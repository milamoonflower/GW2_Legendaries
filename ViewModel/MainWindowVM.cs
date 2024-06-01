﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GW2_Legendaries.Repository;
using GW2_Legendaries.View;
using System.Windows.Controls;

namespace GW2_Legendaries.ViewModel
{
	public class MainWindowVM : ObservableObject
	{
		public ItemCategoriesPage MainPage { get; } = new();
		public ItemListPage ListPage { get; } = new();
		public ItemDescriptionPage DescriptionPage { get; } = new();
		public Page CurrentPage { get; set; }
		public Page PreviousPage { get; set; }

		public RelayCommand SwitchPageCommand { get; }

		public static MainWindowVM? Instance { get; private set; } = null;

		public MainWindowVM()
		{
			CurrentPage = MainPage;
			SwitchPageCommand = new(SwitchPage);
			//ItemRepository.GetItemsAsync(string.Empty);	// Doing this here only because all pages use the same list of items and the amount is not huge
			Instance = this;
		}

		public void SwitchPage()
		{
			if (CurrentPage is ItemCategoriesPage)
			{
				string selectedCategory = string.Empty;

				if (MainPage.DataContext is ItemCategoriesPageVM categoryPageVM)
				{
					selectedCategory = categoryPageVM.SelectedCategory;
				}

				if (ListPage.DataContext is ItemListPageVM listPageVM)
				{
					listPageVM.CurrentCategory = selectedCategory;
					listPageVM.UpdateList(selectedCategory);
				}

				PreviousPage = CurrentPage;
				CurrentPage = ListPage;
			}
			else if (CurrentPage is ItemListPage)
			{
				int selectedItemID = 0;

				if (ListPage.DataContext is ItemListPageVM listPageVM && listPageVM.SelectedItem != null)
				{
					selectedItemID = listPageVM.SelectedItem.ID;
				}
				else
				{
					selectedItemID = ItemListPageVM.Instance.SelectedItem.ID;
				}

				if (DescriptionPage.DataContext is ItemDescriptionPageVM descriptionPageVM)
				{
					descriptionPageVM.CurrentItemID = selectedItemID;
					descriptionPageVM.UpdateCurrentItem();
				}

				PreviousPage = CurrentPage;
				CurrentPage = DescriptionPage;
			}

			OnPropertyChanged(nameof(PreviousPage));
			OnPropertyChanged(nameof(CurrentPage));
		}

		public void GoBack()
		{
			if (CurrentPage is ItemListPage || CurrentPage is ItemDescriptionPage)
			{
				CurrentPage = PreviousPage;
				
				if (CurrentPage is ItemListPage)
				{
					PreviousPage = MainPage;
				}
			}

			OnPropertyChanged(nameof(PreviousPage));
			OnPropertyChanged(nameof(CurrentPage));
		}
	}
}
