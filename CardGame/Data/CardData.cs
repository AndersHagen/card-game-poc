using CardGame.Core.GameElements.GameCards;

namespace CardGame.Data
{
    public class CardData
    {
        public CardId CardId { get; set; }
        public string Image { get; set; }

        public string CardType { get; set; }

        public int Attack { get; set; }

        public int Health { get; set; }

        public string Auras { get; set; }
    }
}
