using UnityEngine;

namespace ProjectCore.Grid
{
    public abstract class GridObjectInstantiator : ScriptableObject
    {
        [SerializeField] protected ProjectGrid CurrentGrid;
        [SerializeField] protected GridObjectView CurrentObj;
        [SerializeField] protected int RequiredTileId;

        protected bool CanInstantiate;
        protected Transform ObjectParent;

        public abstract void CheckForLocation(GridTile currentTile, Transform parent);
        public abstract void InstantiateObject();
        public abstract void DestroyObject();
    }
}
