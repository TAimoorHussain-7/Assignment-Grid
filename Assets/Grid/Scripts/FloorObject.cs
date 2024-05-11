using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "FloorObj", menuName = "Scriptables/Grid/Objects/FloorObj")]
    public class FloorObject : GridObjectInstantiator
    {
        public override void CheckForLocation(GridTile currentTile, Transform parent)
        {
            CanInstantiate = false;
            if (!currentTile.IsOccupied && currentTile.TileId != RequiredTileId)
            {
                StartingTile = currentTile;
                CanInstantiate = true;
            }
            else
            {
                currentTile.RemoveHighlight();
                currentTile.HighlightTile(0);
            }
        }

        public override void InstantiateObject()
        {
            if (CanInstantiate)
            {
                ObjectParent = StartingTile.transform;
                Destroy(ObjectParent.GetChild(2).gameObject);
                Instantiate(CurrentObj, ObjectParent);
                StartingTile.TileId = RequiredTileId;
                CanInstantiate = false;

            }
        }
    }
}
