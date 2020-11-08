using System;
using Task3.Builders;
using Task3.Creator;
using Task3.Model;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 2; i++) {
                var irishCoffeeCreator = new CoffeeCreator(new IrishCoffeeBuilder());
                irishCoffeeCreator.makeCoffee();
                Coffee coffee=irishCoffeeCreator.getCoffee();
                Console.WriteLine("Making : "+coffee.type);
                for (int j = 0; j < coffee.ingredients.Length; j++) {
                    Console.WriteLine(""+ (j+1) +". adding " + coffee.ingredients[j]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                var cappuccinoCreator = new CoffeeCreator(new CappuccinoBuilder());
                cappuccinoCreator.makeCoffee();
                Coffee coffee = cappuccinoCreator.getCoffee();
                Console.WriteLine("Making : " + coffee.type);
                for (int j = 0; j < coffee.ingredients.Length; j++)
                {
                    Console.WriteLine("" + (j + 1) + ". adding " + coffee.ingredients[j]);
                }
            }

            while (true) { }
        }
    }
}
