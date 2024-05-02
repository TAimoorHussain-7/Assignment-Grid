using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "ObjectHolder", menuName = "Scriptables/Grid/ObjectHolder")]

    public class GridObjectHolderSO : ScriptableObject
    {
        public GridObjectInstantiator GridObject;
    }
}
