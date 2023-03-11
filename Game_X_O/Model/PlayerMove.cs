using Microsoft.AspNetCore.Mvc;

namespace Game_X_O.Model
{
    public class PlayerMove
    {
        public int Id { get; set; }
        public int Player { get; set; } = 1;
        public int column { get; set; } = -1;
        public int row { get; set; } = -1;
        public string value { get; set; } = string.Empty;

        //public int GameId { get; set; }

    }
}
