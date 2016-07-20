using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame
{
    //Create a card class with properties of a card
    public class Card
    {
        public int suit;
        public int value;

               //
        public Card(int suit, int value)
        {
            this.suit = suit;
            this.value = value;
        }
    }
}
