using UnityEngine;

namespace Game.Gameplay
{
    public struct FieldGenerationData
    {
        public Vector2Int Size;
        public int BombsAmount;
        public Vector2Int StartPosition;
        public CellModel[,] CellDataArray;
    }
}