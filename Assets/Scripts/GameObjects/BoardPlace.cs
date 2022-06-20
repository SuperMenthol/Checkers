namespace Assets.Scripts.GameObjects
{
    public class BoardPlace
    {
        public float X { get { return _x; } }
        public float Y { get { return _y; } }
        public PieceObject OccupiedBy { get { return _occupiedBy; } }

        private readonly float _x;
        private readonly float _y;
        private PieceObject _occupiedBy;

        public BoardPlace(float x, float y)
        {
            _x = x;
            _y = y;
            _occupiedBy = null;
        }

        public void Place(PieceObject piece) { _occupiedBy = piece; }
    }
}