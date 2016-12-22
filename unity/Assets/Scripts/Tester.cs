using Core.Economy;
using UnityEngine;

public class Tester : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        var cost = new CurrencyCost(Currency.Money, 10) + 
                    new CurrencyCost(Currency.Oil, 1000);
        Debug.Log(cost);
    }
}
