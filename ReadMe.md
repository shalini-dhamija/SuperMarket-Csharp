class Item =>
    ItemName
    Price   

class Inventory => Dictionary of items & quantity
                AddItem
                GetItemByName
                CheckInventoryForOrder
                RemoveItemFromInventory
                ShowCurrentStock
                ItemLookUp

class Order => dictionary with itemname & quantity
                ShowBasket

class SuperMarket => inventory and order
    AddItemsToInventory
    ShowInventory
    ItemLookUp
    AddItemToBasket
    CheckoutOrder
