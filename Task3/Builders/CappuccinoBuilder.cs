using System;
using System.Collections.Generic;
using System.Text;
using Task3.Model;

namespace Task3.Builders
{
    public class CappuccinoBuilder : CaffeeBuilder
    {
        Coffee coffee = new Coffee();

        public Coffee getCoffee()
        {
            return coffee;
        }

        public void setIngredients()
        {
            coffee.ingredients = new String[4] { "water", "coffee", "milk", "white sugar" };

        }

        public void setType()
        {
            coffee.type = "Cappuccino";
        }
    }
}
