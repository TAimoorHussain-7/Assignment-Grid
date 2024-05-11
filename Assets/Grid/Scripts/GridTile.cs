using UnityEngine;

namespace ProjectCore.Grid
{
    public class GridTile: MonoBehaviour
    {
        public int TileId, xIndex, yIndex;
        public bool IsOccupied = false;

        [SerializeField] GameObject HighlightedObj, BlockSign;

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
