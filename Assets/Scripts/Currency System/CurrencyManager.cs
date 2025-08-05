using System;
using UnityEngine;

namespace GameEconomy
{
    public static class CurrencyManager
    {
        public static int CurrentCoin;

        public delegate void CoinsChanged(int current, int difference);
        public static event CoinsChanged OnCoinsChanged;


        public static void ChangeCoins(int increment)
        {
 
            if(increment < 0 && CurrentCoin + increment < 0)
            {
                Debug.LogError("There is an attempt of spending more then what the player has");
                return;
            }

            CurrentCoin += increment;

            Debug.Log("Money: " + CurrentCoin);

            OnCoinsChanged?.Invoke(CurrentCoin, increment);
        }


    }
}

