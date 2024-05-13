using UnityEngine;

namespace ProjectCore.Grid
{
    public class GridTile: MonoBehaviour
    {
        bool _isOccupied = false;

        [SerializeField] GameObject HighlightedObj, BlockSign;
        [SerializeField] BoxCollider TileColider;

        public int TileId, XIndex, YIndex;

        public bool IsOccupied 
        {
            get => _isOccupied;
            set
            {
                TileColider.enabled = !value;
                _isOccupied = value;
            }
        }


        public void HighlightTile(int i)
        {
            if (i == 0)
                BlockSign.SetActive(true);
            else if(i == 1)
                HighlightedObj.SetActive(true);
        }
        public void RemoveHighlight()
        {
            HighlightedObj.SetActive(false);
            BlockSign.SetActive(false);
        }
    }
}
