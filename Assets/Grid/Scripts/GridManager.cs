using UnityEngine;


namespace ProjectCore.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Vector3 TileSize;
        [SerializeField] Transform GridParent;

        [SerializeField] GridCreator CurrentGridCreator;
        [SerializeField] GridDataSO CurrentGridData;



        public void CreateNewGrid()
        {
            CurrentGridCreator.CreateGrid(CurrentGridData,TileSize,GridParent);
        }

    }
}