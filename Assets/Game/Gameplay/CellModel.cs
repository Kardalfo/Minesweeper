namespace Game.Gameplay
{
    public class CellModel
    {
        public bool HasBomb;
        public int AdjacentMineCount;
        public bool IsOpened;
        public bool IsMarked;
        
        public FieldCellView View;
    }
}