using Case.PlayersModule.Models;

namespace Case.TournamentsModule.Models
{
    public class Match
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Match(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
        }
    }
}
