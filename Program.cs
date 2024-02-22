
SuperMarket mySuperMarket = new SuperMarket();

Console.WriteLine("Are you a customer or an employee?");
if (Enum.TryParse<Role>(Console.ReadLine(), ignoreCase: true, out var role))
{
    if (role == Role.Employee)
    {
        while(true){
            Console.WriteLine("Welcome, employee.");
            Console.WriteLine("*****");
            Console.WriteLine("[1] Look up a product");
            Console.WriteLine("[2] Register new stock");
            Console.WriteLine("[3] See full inventory");
            Console.WriteLine("Enter the number of the action you would like to take:");
            if (int.TryParse(Console.ReadLine(), out var actionNumber))
            {
                if (actionNumber == 1)
                {
                    Console.WriteLine("Please enter the item you want to look up: ");
                    var itemName = Console.ReadLine() ?? "";
                    mySuperMarket.ItemLookUp(itemName);
                    continue;
                }
                else if (actionNumber == 2)
                {
                    var addMore = true;
                    while (addMore)
                    {
                        Console.WriteLine("Please enter the item name: ");
                        var itemName = Console.ReadLine() ?? "";
                        Console.WriteLine("Please enter the item quantity: ");
                        int.TryParse(Console.ReadLine(), out int itemQuantity);
                        Console.WriteLine("Please enter the item price: ");
                        decimal.TryParse(Console.ReadLine(), out decimal itemPrice);                      
                        mySuperMarket.AddItemsToInventory(itemName,itemQuantity,itemPrice);
                        Console.WriteLine("Do you want to enter more items to basket? (Y/N)");
                            var response = Console.ReadLine() ?? "";
                            if (response.ToLower() != "y") 
                                addMore = false;
                    }
                    continue;
                }
                else if (actionNumber == 3)
                {
                    mySuperMarket.ShowInventory();
                }
                else
                {
                    Console.WriteLine("Sorry, that wasn't one of the options.");
                }
            }
            else
            {
                Console.WriteLine("Sorry, I didn't understand your response.");
            }
            break;
        }
    }
    else if (role == Role.Customer)
    {
        mySuperMarket.AddItemsToInventory("Apple", 10, 0.5m);
        mySuperMarket.AddItemsToInventory("Banana", 10, 0.2m);
        mySuperMarket.AddItemsToInventory("Cake", 20, 2.0m);
        mySuperMarket.AddItemsToInventory("Milk", 30, 1.5m);
        mySuperMarket.AddItemsToInventory("Egg Box", 10, 3m);
        mySuperMarket.AddItemsToInventory("Cucumber", 30, 0.75m);
        Console.WriteLine("Welcome, customer. You have been given an empty basket.");
        while (true)
        {
            Console.WriteLine("*****");
            Console.WriteLine("[1] Add an item to the basket");
            Console.WriteLine("[2] Check out");
            Console.WriteLine("Enter the number of the action you would like to take next:");
            if (int.TryParse(Console.ReadLine(), out var actionNumber))
            {
                if (actionNumber == 1)
                {
                    var addMore = true;
                    while (addMore)
                    {
                        Console.WriteLine("Enter the item name you would like to add to the basket: ");
                        var itemName = Console.ReadLine() ?? "";
                        Console.WriteLine("Please enter the item quantity: ");
                        int.TryParse(Console.ReadLine(), out int itemQuantity);
                        mySuperMarket.AddItemToBasket(itemName, itemQuantity);
                        Console.WriteLine("Do you want to enter more items to basket? (Y/N)");
                        var response = Console.ReadLine() ?? "";
                        if (response.ToLower() != "y") 
                            addMore = false;
                    }
                    continue;
                }
                else if (actionNumber == 2)
                {
                    mySuperMarket.CheckoutOrder();
                }
                else
                {
                    Console.WriteLine("Sorry, that wasn't one of the options.");
                }
            }
            else
            {
                Console.WriteLine("Sorry, I didn't understand your response.");
            }
            break;
        }
    }
}
else
{
    Console.WriteLine("Sorry, I didn't understand your response.");
}


// mySuperMarket.AddItemsToInventory("Apple", 10, 0.5m);
// mySuperMarket.AddItemsToInventory("Banana", 10, 0.2m);
// mySuperMarket.AddItemsToInventory("Cake", 20, 2.0m);
// mySuperMarket.AddItemsToInventory("Milk", 30, 1.5m);
// mySuperMarket.AddItemsToInventory("Egg Box", 10, 3m);
// mySuperMarket.AddItemsToInventory("Cucumber", 30, 0.75m);

// mySuperMarket.ShowInventory();

// //mySuperMarket.ItemLookUp("Apple");
// //mySuperMarket.ItemLookUp("Machine");

// mySuperMarket.AddItemToBasket("Apple", 2);
// mySuperMarket.AddItemToBasket("Cake", 1);
// mySuperMarket.AddItemToBasket("Banana", 11);
// mySuperMarket.AddItemToBasket("Robot", 11);
// mySuperMarket.CheckoutOrder();

// mySuperMarket.ShowInventory();

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

enum Role
{
    Customer,
    Employee,
}