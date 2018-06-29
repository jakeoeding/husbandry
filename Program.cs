using Husbandry.Logic;

namespace Husbandry
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Setup();
            game.gameplay.Play();
            game.UpdateHighscores();
        }
    }
}
