using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Game/GameFieldData")]
    public class InitialFieldData : ScriptableObject
    {
        public Vector2Int Size;
        public int BombsAmount;
    }
}