using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace Inventory
{
	public class InventorySystem : MonoBehaviour
	{
        #region Fields
        [Header("UI Reffs")]
		[SerializeField] private UIController _ui;

		[Header("Object Definition")]
		[SerializeField] private Weapon[] _weapons;
		[SerializeField] private Food[] _foods;
		[SerializeField] private Other[] _others;
		[Header("Item Pool")]
		[SerializeField] private List<Item> _items = new List<Item>();

		#endregion

		#region Unity Callbacks
		// Start is called before the first frame update
		void Start()
		{
			_ui.OnSell += OnSellItem;
			_ui.OnUse += OnUseItem;
        }
		
		#endregion

		#region Public Methods
		public List<Item> InitializeItems()
		{
			//Weapons
			for (int i = 0; i < _weapons.Length; i++)
				_items.Add(_weapons[i]);

			//Food
			for (int i = 0; i < _foods.Length; i++)
				_items.Add(_foods[i]);

			//Other
			for (int i = 0; i < _others.Length; i++)
				_items.Add(_others[i]);
			return _items;
        }
		#endregion

		#region Private Methods
		private void OnSellItem()
		{
			(_ui.CurrentItemSelected.CurrentItem as ISellable).Sell();
			Consume(_ui.CurrentItemSelected);
		}

		private void OnUseItem()
		{
			(_ui.CurrentItemSelected.CurrentItem as IUsable).Use();
			if (_ui.CurrentItemSelected.CurrentItem is IConsumable)
				Consume(_ui.CurrentItemSelected);
		}
		private void Consume(ItemButtom currentItemSelected)
		{
			Destroy(currentItemSelected.gameObject);
		}


		#endregion
	}
}
