using UnityEngine;
using UnityEngine.Pool;

namespace Game.Gameplay
{
    public class FieldGenerator : MonoBehaviour
    {
        [SerializeField] private FieldCellView _cellViewPrefab;
        [SerializeField] private Transform _fieldParent;
        [SerializeField] private float _spacingValue;

        private ObjectPool<FieldCellView> _cellPool;


        private void Awake()
        {
            _cellPool = new ObjectPool<FieldCellView>(() => Instantiate(_cellViewPrefab, transform),
                cell => cell.gameObject.SetActive(true),
                cell => cell.gameObject.SetActive(false),
                cell => Destroy(cell.gameObject),
                true,
                10,
                200
            );
        }

        public CellModel[,] GenerateField(Vector2Int size)
        {
            var activeCells = new CellModel[size.x, size.y];
            
            var offsetX = (size.x - 1) * _spacingValue / 2f;
            var offsetY = (size.y - 1) * _spacingValue / 2f;
            
            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y< size.y; y++)
                {
                    var cellView = _cellPool.Get();
                    cellView.transform.SetParent(_fieldParent);
                    cellView.transform.localPosition = new Vector3(x * _spacingValue - offsetX, y * _spacingValue - offsetY, 0);
                    cellView.ResetCell();
                    activeCells[x, y] = new CellModel
                    {
                        View = cellView,
                    };
                    
                    cellView.SetGridPosition(new Vector2Int(x, y));
                }
            }

            return activeCells;
        }

        public void ResetField(CellModel[,] cellDataArray)
        {
            foreach (var cellData in cellDataArray)
            {
                var cellView = cellData.View;
                _cellPool.Release(cellView);
            }
        }
    }
}