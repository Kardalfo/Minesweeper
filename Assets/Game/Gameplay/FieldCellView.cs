using TMPro;
using UnityEngine;

namespace Game.Gameplay
{
    public class FieldCellView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberTmp;
        [SerializeField] private GameObject _markObject;
        [SerializeField] private GameObject _bombObject;
        [SerializeField] private GameObject _capObject;
        
        public Vector2Int GridPosition { get; private set; }


        private void Awake()
        {
            ResetCell();
        }

        public void Press(CellModel cellModel)
        {
            if (cellModel.HasBomb)
            {
                _bombObject.SetActive(true);
            }
            else if (cellModel.AdjacentMineCount > 0)
            {
                _numberTmp.gameObject.SetActive(true);
                _numberTmp.text = cellModel.AdjacentMineCount.ToString();
            }
            _markObject.SetActive(false);
            _capObject.SetActive(false);
        }

        public void Mark(bool isMarked)
        {
            _markObject.SetActive(isMarked);
        }

        public void ResetCell()
        {
            _numberTmp.gameObject.SetActive(false);
            _markObject.SetActive(false);
            _bombObject.SetActive(false);
            _capObject.SetActive(true);
        }

        public void SetGridPosition(Vector2Int position)
        {
            GridPosition = position;
        }
    }
}