using UnityEngine;

namespace ProjectCore.Grid
{
    public class GridObjectView : MonoBehaviour
    {
        [SerializeField] SpriteRenderer ObjectBody;
        [SerializeField] BoxCollider ObjectCollider;
        
        GridTile[] _objectLocation;

        public void ActiveObject(GridTile[] objectLocation)
        {
            _objectLocation = objectLocation;
            foreach (GridTile tile in _objectLocation)
            {
                tile.IsOccupied = true;
            }
            ShowFullView();
            ObjectCollider.enabled = true;
        }

        public void ShowFullView()
        {
            Color color = ObjectBody.color;
            color.a = 1;
            ObjectBody.color = color;
        }

        public void HideObject()
        {
            Color color = ObjectBody.color;
            color.a = 0;
            ObjectBody.color = color;
        }

        public void HighlightObject()
        {
            Debug.Log("Hiegh");
            Color color = ObjectBody.color;
            color.a = 0.5f;
            ObjectBody.color = color;
        }

        public void RemoveObject()
        {
            //Debug.Log("Here");
            foreach (GridTile tile in _objectLocation)
            {
                tile.IsOccupied = false;
            }
            Destroy(this.gameObject);
        }
    }
}
