namespace CardGame.Core
{
    public interface IClickable
    {
        bool IsClicked(int x, int y);

        public IClickable OnClick(int x, int y);
    }
}
