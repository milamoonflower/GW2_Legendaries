﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GW2_Legendaries.Model;
using GW2_Legendaries.Repository;

namespace GW2_Legendaries.ViewModel
{
	public class ItemDescriptionPageVM : ObservableObject
	{
		public RelayCommand GoBackCommand { get; }
		public int CurrentItemID { get; set; } = 0;
		private Item? m_CurrentItem = new();

		public Item? CurrentItem
		{
			get => m_CurrentItem;
			set
			{
				m_CurrentItem = value;
				OnPropertyChanged(nameof(m_CurrentItem));
			}
		}

		public ItemDescriptionPageVM()
		{
			GoBackCommand = new RelayCommand(GoBack);
		}

		public void UpdateCurrentItem()
		{
			CurrentItem = ItemRepository.GetItemWithID(CurrentItemID);

			OnPropertyChanged(nameof(CurrentItem));
			OnPropertyChanged(nameof(CurrentItemID));
		}

		private void GoBack()
		{
			MainWindowVM.Instance?.GoBack();
		}
	}
}
