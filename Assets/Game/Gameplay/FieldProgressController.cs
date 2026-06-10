using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class FieldProgressController
    {
        public event Action NoCellsLeftEvent;
        public event Action BombPressedEvent;
        
        private readonly FieldInputController _inputController;
        
        private CellModel[,] _cellDataArray;
        
        private Vector2Int _fieldSize;
        private int _cellsToOpen;


        public FieldProgressController(FieldInputController inputController)
        {
            _inputController = inputController;
        }

        public void Prepare(CellModel[,] cellDataArray)
        {
            _cellDataArray = cellDataArray;
            _inputController.MarkRequestedEvent += HandleMarkRequested;
        }

        public void Start(Vector2Int size, int bombsAmount)
        {
            _fieldSize = size;
            _cellsToOpen = size.x * size.y - bombsAmount;
            
            _inputController.CellPressedEvent += HandleCellPressed;
        }

        private void HandleMarkRequested(Vector2Int gridPosition)
        {
            var cell = _cellDataArray[gridPosition.x, gridPosition.y];
            if (cell.IsOpened)
                return;
            
            var cellView = cell.View;
            var needMark = !cell.IsMarked;
            
            cell.IsMarked = needMark;
            cellView.Mark(needMark);
        }

        public void HandleCellPressed(Vector2Int gridPosition)
        {
            try
            {
                var cell = _cellDataArray[gridPosition.x, gridPosition.y];

                if (cell.IsOpened || cell.IsMarked)
                {
                    return;
                }

                if (cell.HasBomb)
                {
                    ShowBombs();
                    BombPressedEvent?.Invoke();
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return;
            }
            
            ShowCells(gridPosition);
            
            if (_cellsToOpen <= 0)
            {
                ShowMarks();
                NoCellsLeftEvent?.Invoke();
            }
        }

        private void ShowCells(Vector2Int startPosition)
        {
            var queue = new Queue<Vector2Int>();
            var hashtable = new HashSet<Vector2Int>();

            queue.Enqueue(startPosition);

            while (queue.Count > 0)
            {
                var position = queue.Dequeue();
                if (hashtable.Add(position) == false)
                    continue;
                
                var cell = _cellDataArray[position.x, position.y];

                if (cell.IsOpened || (cell.IsMarked && cell.HasBomb))
                    continue;

                cell.IsOpened = true;
                cell.View.Press(cell);
                
                _cellsToOpen -= 1;
                
                if (cell.AdjacentMineCount > 0)
                    continue;

                foreach (var neighbourPos in GridUtils.GetNeighbors(position, _fieldSize))
                {
                    var neighbour = _cellDataArray[neighbourPos.x, neighbourPos.y];

                    if (neighbour.IsOpened ||
                        neighbour.IsMarked && neighbour.HasBomb ||
                        neighbour.HasBomb)
                        continue;

                    queue.Enqueue(neighbourPos);
                }
            }
        }

        private void ShowBombs()
        {
            foreach (var cellModel in _cellDataArray)
            {
                if (cellModel.HasBomb)
                    cellModel.View.Press(cellModel);
            }
        }

        private void ShowMarks()
        {
            foreach (var cellModel in _cellDataArray)
            {
                if (cellModel.HasBomb && cellModel.IsOpened == false)
                    cellModel.View.Mark(true);
            }
        }

        public void Clear()
        {
            _cellDataArray = null;
            
            NoCellsLeftEvent = null;
            BombPressedEvent = null;
        }
    }
}