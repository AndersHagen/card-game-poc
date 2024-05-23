using CardGame.Core.GameElements.GameCards;
using System.Collections.Generic;

namespace CardGame.Data
{
    public class CardInfo
    {
        public CardId CardId { get; set; }
        public string Image { get; set; }

        public string CardType { get; set; }

        public int Attack { get; set; }

        public int Health { get; set; }

        public List<Aura> Auras { get; set; }

        public CardInfo(CardData data, List<Aura> auras) 
        {
            CardId = data.CardId;
            Image = data.Image;
            CardType = data.CardType;
            Attack = data.Attack;
            Health = data.Health;
            Auras = auras;
        }
    }
}
