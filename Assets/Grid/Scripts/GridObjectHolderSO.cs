using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "ObjectHolder", menuName = "Scriptables/Grid/ObjectHolder")]

    public class GridObjectHolderSO : ScriptableObject
    {
        [SerializeField] GridObjectInstantiator _gridObj;
        public GridObjectInstantiator GridObject 
        { get=> _gridObj; set => _gridObj = value; }
    }
}
