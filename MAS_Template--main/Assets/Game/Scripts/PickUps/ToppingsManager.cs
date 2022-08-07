using System;
using UnityEngine;


public class ToppingsManager : MonoBehaviour
{
    [SerializeField] private Toppings toppingType;

    public void ApplyTopping(DoughTopping topping)
    {
        switch (toppingType)
        {
            case Toppings.Chocolate:
                topping.EnableChocolate();
                break;
            case Toppings.Cream:
                topping.EnableCream();
                break;
            case Toppings.Icing:
                topping.EnableIcing();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
