using UnityEngine;

namespace ProjectCore.Grid
{
    public abstract class GridObjectInstantiator : ScriptableObject
    {
        [SerializeField] protected ProjectGrid CurrentGrid;
        [SerializeField] protected GameObject CurrentObj;
        [SerializeField] protected int RequiredTileId;

        protected bool CanInstantiate;

        public abstract void CheckForLocation(GridTile currentTile);
        public abstract void InstantiateObject(Transform parent);
    }
}
