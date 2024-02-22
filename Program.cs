
SuperMarket mySuperMarket = new SuperMarket();

mySuperMarket.AddItemsToInventory("Apple", 10, 0.5m);
mySuperMarket.AddItemsToInventory("Banana", 10, 0.2m);
mySuperMarket.AddItemsToInventory("Cake", 20, 2.0m);
mySuperMarket.AddItemsToInventory("Milk", 30, 1.5m);
mySuperMarket.AddItemsToInventory("Egg Box", 10, 3m);
mySuperMarket.AddItemsToInventory("Cucumber", 30, 0.75m);

mySuperMarket.ShowInventory();

//mySuperMarket.ItemLookUp("Apple");
//mySuperMarket.ItemLookUp("Machine");

mySuperMarket.AddItemToBasket("Apple", 2);
mySuperMarket.AddItemToBasket("Cake", 1);
mySuperMarket.AddItemToBasket("Banana", 11);
mySuperMarket.AddItemToBasket("Robot", 11);
mySuperMarket.CheckoutOrder();

mySuperMarket.ShowInventory();

class SuperMarket
{
    private Inventory _myInventory = new Inventory(); 
    private Order _myOrder = new Order();

    public void AddItemsToInventory(string itemName, int quantity, decimal price)
    {
        Item item = new Item{ItemName=itemName, Price=price};
        _myInventory.AddItem(item,quantity);       
    }

    public void ShowInventory()
    {
        _myInventory.ShowCurrentStock();
    }

    public void ItemLookUp(string itemName)
    {
        _myInventory.ItemLookUp(itemName);
    }

    public void AddItemToBasket(string itemName, int quantity)
    {
        _myOrder.DictBasket.Add(itemName,quantity);
        Console.WriteLine($"{quantity} {itemName} added to basket.");
    }

    public void CheckoutOrder()
    {
        var totalPrice = 0m;
        Console.WriteLine("Checking Stock....");
        _myInventory.CheckInventoryForOrder(_myOrder.DictBasket);
        Console.WriteLine("Do you want to proceed? (Y/N)");
        var response = Console.ReadLine()?? "";
        if(response.ToLower() == "y")
        {
            foreach(var orderItem in _myOrder.DictBasket)
            {
                var dictItem = _myInventory.GetItemByName(orderItem.Key);
                totalPrice += dictItem.Price * orderItem.Value;
                _myInventory.RemoveItemFromInventory(orderItem.Key, orderItem.Value);
            }
        }
        Console.WriteLine($"Your order is ready, please pay £{totalPrice}");
    }
}