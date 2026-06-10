using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public static class GridUtils
    {
        public static IEnumerable<Vector2Int> GetNeighbors(Vector2Int position, Vector2Int gridSize)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                for (var dx = -1; dx <= 1; dx++)
                {
                    if (dx == 0 && dy == 0) continue;

                    var neighborPos = new Vector2Int(position.x + dx, position.y + dy);

                    if (neighborPos.x >= 0 && neighborPos.x < gridSize.x &&
                        neighborPos.y >= 0 && neighborPos.y < gridSize.y)
                    {
                        yield return neighborPos;
                    }
                }
            }
        }
    }
}
