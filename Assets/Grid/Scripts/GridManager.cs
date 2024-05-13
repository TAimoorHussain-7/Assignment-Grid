using UnityEngine;
using ProjectCore.Events;
using ProjectCore.Variables;


namespace ProjectCore.Grid
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Vector3 TileSize;
        [SerializeField] Transform TilesParent, BuildingsParent;
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
                foreach (Transform tile in TilesParent)
                {
                    Destroy(tile.gameObject);
                }
                foreach (Transform tile in BuildingsParent)
                {
                    Destroy(tile.gameObject);
                }
                CurrentGridCreator.CreateGrid(CurrentGridJson.CurrentJsonData, TileSize, TilesParent, BuildingsParent);
            }
        }
    }
}