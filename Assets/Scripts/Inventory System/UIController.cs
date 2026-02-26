using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace Inventory
{
    public class UIController : MonoBehaviour
    {
        #region Properties
        public ItemButtom CurrentItemSelected { get; set; }

        public event System.Action SellAction;
        public event System.Action UseAction;
        #endregion

        #region Fields
        [SerializeField] private ItemButtom _prefabButton;
        [SerializeField] private Transform _itemPoolPanel;
        [SerializeField] private Transform _inventoryPanel;
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private List<Item> _items = new List<Item>();
        [SerializeField] private InventorySystem _inventorySystem;
        #endregion

        #region Unity Callbacks
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // Initialize inventory and UI
            _items = _inventorySystem.InitializeItems();
            InitializeUI(_items);

            // Add listeners to buttons
            _useButton.onClick.AddListener(UseCurrentItem);
            _sellButton.onClick.AddListener(SellCurrentItem);
        }
        #endregion

        #region Public Methods
        // Adds an item to the inventory UI and sets up its click event to select the item
        public void AddItem(ItemButtom buttonItemToAdd)
        {
            ItemButtom newButtonItem = Instantiate(buttonItemToAdd, _inventoryPanel);
            newButtonItem.CurrentItem = buttonItemToAdd.CurrentItem;
            newButtonItem.OnClick += () => SelecItem(newButtonItem);
        }
        // Selects an item and updates the UI based on its properties (sellable, usable)
        public void SelecItem(ItemButtom currentItem)
        {
            CurrentItemSelected = currentItem;
            //If Sellable
            if (CurrentItemSelected.CurrentItem is ISellable)
                _sellButton.gameObject.SetActive(true);
            else
                _sellButton.gameObject.SetActive(false);

            //If Usable
            if (CurrentItemSelected.CurrentItem is IUsable)
                _useButton.gameObject.SetActive(true);
            else
                _useButton.gameObject.SetActive(false);
        }
        #endregion

        #region Private Methods
        // Initializes the UI by creating buttons for each item in the item pool and setting up their click events to add the item to the inventory
        private void InitializeUI(List<Item> items)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                ItemButtom newButton = Instantiate(_prefabButton, _itemPoolPanel);
                newButton.name = _items[i].Name + "Button";
                newButton.gameObject.SetActive(true);
                newButton.CurrentItem = _items[i];
                newButton.OnClick += () => AddItem(newButton);
            }
            _prefabButton.gameObject.SetActive(false);
        }

        // Invokes the sell action for the currently selected item and then deselects it
        private void SellCurrentItem()
        {
            SellAction?.Invoke();
            DeselectItem();
        }

        // Invokes the use action for the currently selected item and then deselects it
        private void UseCurrentItem()
        {
            UseAction?.Invoke();
            DeselectItem();
        }

        // Deselects the currently selected item and hides the sell and use buttons
        private void DeselectItem()
        {
            CurrentItemSelected = null;
            _sellButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
        }

        #endregion

        #region Gizmos
        #endregion
    }
}
