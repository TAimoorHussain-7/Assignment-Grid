using UnityEngine;
using ProjectCore.Variables;


namespace ProjectCore.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Vector3 TileSize;
        [SerializeField] Transform GridParent;
        [SerializeField] SOBool CreatingGrid;
        [SerializeField] GridCreator CurrentGridCreator;
        [SerializeField] GridDataSO CurrentGridData;



        public void CreateNewGrid()
        {
            if (!CreatingGrid.Value)
            {
                CreatingGrid.Value = true;
                foreach (Transform tile in GridParent)
                {
                    Destroy(tile.gameObject);
                }
                CurrentGridCreator.CreateGrid(CurrentGridData, TileSize, GridParent);
            }
        }

    }
}