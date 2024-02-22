public class Inventory
{
    private Dictionary<Item, int> _dictInventory = new Dictionary<Item, int>();

    public void AddItem(Item item, int quantity)
    {
        if (_dictInventory.ContainsKey( item )) 
        {
            _dictInventory[item] += quantity;
        }
        else
        {
            _dictInventory.Add(item, quantity);
        }
    }

    public Item GetItemByName(string itemName)
    {
        return _dictInventory.First(i => i.Key.ItemName == itemName).Key;
    }

    public void RemoveItemFromInventory(string itemName, int quantity) 
    {
        var item = _dictInventory.First(i => i.Key.ItemName == itemName).Key;
         _dictInventory[item] -= quantity;        
    }

    public void CheckInventoryForOrder(Dictionary<string,int> orderList)
    {
        foreach(var orderItem in orderList)  //Check if there is stock for eack item ordered
        {
            if(_dictInventory.Any(dictItem => dictItem.Key.ItemName == orderItem.Key)) //If Item exists in inventory
            {
                var dictItem = _dictInventory.First(i => i.Key.ItemName == orderItem.Key);
                if(dictItem.Value > orderItem.Value) //Quantity asked is in stock
                {
                    Console.WriteLine($"{orderItem.Value} {orderItem.Key} in stock.");
                }
                else
                {
                    Console.WriteLine($"{orderItem.Value} {orderItem.Key} not in stock. Quantity of item {orderItem.Key} has been updated to {dictItem.Value}");
                    orderList[orderItem.Key] = dictItem.Value;
                }
            }
            else
            {
                orderList.Remove(orderItem.Key);
                Console.WriteLine($"Sorry we don't sell {orderItem.Key}, this hs been removed from basket.");
            }
        }        
    }

    public void ShowCurrentStock()
    {
        Console.WriteLine("--------Current Stock--------");
        foreach(var item in _dictInventory)
            Console.WriteLine($"{item.Key.ItemName} - {item.Value}");
    }

    public void ItemLookUp(string itemName)
    {
        Console.WriteLine($"--------{itemName}--------");
        if(_dictInventory.Any(dictItem => dictItem.Key.ItemName == itemName)) //If Item exists in inventory
        {   
            (var item,var quantity) = _dictInventory.First(item => item.Key.ItemName == itemName);
            Console.WriteLine($"Name: {item.ItemName} Quantity: {quantity} Price: {item.Price}");
            }
        else
            Console.WriteLine($"{itemName} not in inventory. ");
    }

}

public class Item
{
    public required string ItemName {get;set;}    
    public required decimal Price{get;set;}
}