using UnityEngine;

namespace Game.Gameplay
{
    public class FieldFiller
    {
        public void GenerateContent(FieldGenerationData data)
        {
            var sizeX = data.Size.x;
            var sizeY = data.Size.y;
            
            var random = new System.Random();
            var placedBombs = 0;
            
            var cellDataArray = data.CellDataArray;
            var startX = data.StartPosition.x;
            var startY = data.StartPosition.y;

            var targetBombsAmount = data.BombsAmount;
            if (data.BombsAmount > cellDataArray.Length)
            {
                targetBombsAmount = cellDataArray.Length - 1;
                Debug.LogWarning($"There is too much bombs amount written in the config, set to {targetBombsAmount}");
            }
            
            while (placedBombs < targetBombsAmount)
            {
                var x = random.Next(0, sizeX);
                var y = random.Next(0, sizeY);
                
                if (x == startX && y == startY || cellDataArray[x, y].HasBomb) 
                    continue;
                
                var cell = cellDataArray[x, y];
                cell.HasBomb = true;
                cellDataArray[x, y] = cell;
                
                placedBombs++;
            }
            
            for (var y = 0; y < data.Size.y; y++)
            {
                for (var x = 0; x < data.Size.x; x++)
                {
                    var pos = new Vector2Int(x, y);
                    var cell = cellDataArray[x, y];

                    if (cell.HasBomb) 
                        continue;
                    
                    var bombsCount = 0;
                    var neighborPositions = GridUtils.GetNeighbors(pos, data.Size);
                    foreach (var neighborPosition in neighborPositions)
                    {
                        var neighborCell = cellDataArray[neighborPosition.x, neighborPosition.y];
                        if (neighborCell.HasBomb)
                        {
                            bombsCount++;
                        }
                    }
                        
                    cell.AdjacentMineCount = bombsCount;
                    cellDataArray[x, y] = cell;
                }
            }
        }
    }
}