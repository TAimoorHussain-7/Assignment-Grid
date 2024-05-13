using UnityEngine;

namespace ProjectCore.Grid
{
    public abstract class GridObjectInstantiator : ScriptableObject
    {
        [SerializeField] protected GameObject CurrentObj;
        [SerializeField] protected int RequiredTileId;

        protected bool CanInstantiate;
        protected GridTile StartingTile;
        protected Transform ObjectParent;

        public abstract void CheckForLocation(GridTile currentTile, Transform parent);
        public abstract void InstantiateObject();
    }
}
