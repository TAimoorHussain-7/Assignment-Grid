using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "GridJson", menuName = "Scriptables/Grid/GridJson")]
    public class GridDataSO : ScriptableObject
    {
        public GridRow[] GridRows;
    }
}
