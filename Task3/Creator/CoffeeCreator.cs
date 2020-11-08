using System;
using System.Collections.Generic;
using System.Text;
using Task3.Builders;
using Task3.Model;

namespace Task3.Creator
{
    public class CoffeeCreator
    {
        CaffeeBuilder builder;

        public CoffeeCreator(CaffeeBuilder builder) { this.builder = builder; }

        public void makeCoffee() {
            builder.setType();
            builder.setIngredients();
        }

        public Coffee getCoffee() { return builder.getCoffee(); }
    }
}
