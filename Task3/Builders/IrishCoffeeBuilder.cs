using System;
using System.Collections.Generic;
using System.Text;
using Task3.Model;

namespace Task3.Builders
{
    public class IrishCoffeeBuilder : CaffeeBuilder
    {
        Coffee coffee = new Coffee();

        public Coffee getCoffee()
        {
            return coffee;
        }

        public void setIngredients()
        {
            coffee.ingredients = new String[4] { "brown sugar", "coffee", " Irish whiskey", "fresh cream" };
        }

        public void setType()
        {
            coffee.type = "Irish coffee";
        }
    }
}
