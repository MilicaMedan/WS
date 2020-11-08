using System;
using System.Collections.Generic;
using System.Text;
using Task3.Model;

namespace Task3.Builders
{
    public interface CaffeeBuilder
    {
        void setType();
        void setIngredients();
        Coffee getCoffee();
    }
}
