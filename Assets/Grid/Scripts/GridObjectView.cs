using UnityEngine;

namespace ProjectCore.Grid
{
    public class GridObjectView : MonoBehaviour
    {
        [SerializeField] SpriteRenderer ObjectBody;
        
        GridTile[] _objectLocation;

        public void ActiveObject(GridTile[] objectLocation)
        {
            _objectLocation = objectLocation;
            foreach (GridTile tile in _objectLocation)
            {
                tile.IsOccupied = true;
            }
            Color color = ObjectBody.color;
            color.a = 1;
            ObjectBody.color = color;
        }
    }
}
