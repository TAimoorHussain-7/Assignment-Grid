using UnityEngine;
using ProjectCore.Events;
using ProjectCore.Variables;


namespace ProjectCore.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Vector3 TileSize;
        [SerializeField] Transform GridParent;
        [SerializeField] SOBool CreatingGrid;
        [SerializeField] SOEvents CreateGridEvent;
        [SerializeField] GridCreator CurrentGridCreator;
        [SerializeField] GridJsonDataSO CurrentGridJson;

        private void OnEnable()
        {
            CreateGridEvent.Handler += CreateNewGrid;
        }

        private void OnDisable()
        {
            CreateGridEvent.Handler -= CreateNewGrid;
        }

        private void CreateNewGrid()
        {
            if (!CreatingGrid.Value)
            {
                CreatingGrid.Value = true;
                foreach (Transform tile in GridParent)
                {
                    Destroy(tile.gameObject);
                }
                CurrentGridCreator.CreateGrid(CurrentGridJson.CurrentJsonData, TileSize, GridParent);
            }
        }
    }
}