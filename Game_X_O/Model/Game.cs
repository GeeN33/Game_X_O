namespace Game_X_O.Model
{
    public class Game
    {
        public int Id { get; set; }
        public int PlayerTurn { get; set; } = 1;
        public int Winner { get; set; } = 1;
        public string Stage { get; set; } = string.Empty;
        public string error { get; set; } = string.Empty;

        public List<PlayerMove> PlayerMoves { get; set; } = new();
      
    }


}
