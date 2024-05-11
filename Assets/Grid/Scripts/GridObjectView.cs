using UnityEngine;

namespace ProjectCore.Grid
{
    public class GridObjectView : MonoBehaviour
    {
        [SerializeField] SpriteRenderer ObjectBody;
        [SerializeField] BoxCollider ObjectCollider;
        [SerializeField] GridDataSO CurrentGrid;
        
        GridBuilingBlock _myBlock;

        public void ActiveObject(GridBuilingBlock myBlock)
        {
            _myBlock = myBlock;
            ChangeTilesState(true);
            CurrentGrid.GridBuildings.Add(_myBlock);
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
            //Debug.Log("Hiegh");
            Color color = ObjectBody.color;
            color.a = 0.5f;
            ObjectBody.color = color;
        }

        public void RemoveObject()
        {
            //Debug.Log("Here");
            ChangeTilesState(false);
            CurrentGrid.GridBuildings.Remove(_myBlock);
            Destroy(this.gameObject);
        }

        void ChangeTilesState(bool State)
        {
            foreach (Vector2Int tileIndex in _myBlock.TilesOccupied)
            {
                CurrentGrid.GridRows[tileIndex.x].TilesRow[tileIndex.y].IsOccupied = State;
            }
        }
    }
}
