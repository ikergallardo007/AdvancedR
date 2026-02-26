using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class UIController : MonoBehaviour
    {
        #region Properties
        #endregion

        #region Fields
        [SerializeField] private ItemButtom _prefabButton;
        [SerializeField] private Transform _itemPoolPanel;
        [SerializeField] private Transform _inventoryPanel;
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private List<Item> _items = new List<Item>();
        [SerializeField] private ItemButtom _currentItemSelected;
        [SerializeField] private InventorySystem _inventorySystem;
        #endregion

        #region Unity Callbacks
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _items = _inventorySystem.InitializeItems();
            InitializeUI(_items);

            _useButton.onClick.AddListener(UseCurrentItem);
            _sellButton.onClick.AddListener(SellCurrentItem);
        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region Public Methods
        public void AddItem(ItemButtom buttonItemToAdd)
        {
            ItemButtom newButtonItem = Instantiate(buttonItemToAdd, _inventoryPanel);
            newButtonItem.CurrentItem = buttonItemToAdd.CurrentItem;
            newButtonItem.OnClick += () => SelecItem(newButtonItem);
        }
        public void SelecItem(ItemButtom currentItem)
        {
            _currentItemSelected = currentItem;
            //If Sellable
            if (_currentItemSelected.CurrentItem is ISellable)
                _sellButton.gameObject.SetActive(true);
            else
                _sellButton.gameObject.SetActive(false);

            //If Usable
            if (_currentItemSelected.CurrentItem is IUsable)
                _useButton.gameObject.SetActive(true);
            else
                _useButton.gameObject.SetActive(false);
        }
        #endregion

        #region Private Methods
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

        private void SellCurrentItem()
        {
            (_currentItemSelected.CurrentItem as ISellable).Sell();
            _inventorySystem.Consume(_currentItemSelected);
            _currentItemSelected = null;
            _sellButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
        }

        private void UseCurrentItem()
        {
            (_currentItemSelected.CurrentItem as IUsable).Use();
            if (_currentItemSelected.CurrentItem is IConsumable)
                _inventorySystem.Consume(_currentItemSelected);
            _currentItemSelected = null;
            _sellButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
        }

        #endregion

        #region Gizmos
        #endregion
    }
}
