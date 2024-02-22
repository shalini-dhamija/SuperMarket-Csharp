public class Order
{
    public Dictionary<string, int> DictBasket{get;} = new Dictionary<string, int>();

    public void ShowBasket()
    {
        Console.WriteLine("--------Basket--------");
        foreach(var item in DictBasket)
            Console.WriteLine($"{item.Key} - {item.Value}");
    }

}