using UnityEngine;

namespace ProjectCore.Grid
{
    public class GridTile: MonoBehaviour
    {
        public int TileId, xIndex, yIndex;
        public bool IsOccupied = false;

        [SerializeField] GameObject HighlightedObj;

        public void HighlightTile()
        {
            HighlightedObj.SetActive(true);
        }
        public void RemoveHighlight()
        {
            HighlightedObj.SetActive(false);
        }
    }
}
