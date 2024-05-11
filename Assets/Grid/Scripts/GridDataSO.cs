using UnityEngine;
using System.Collections.Generic;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "GridData", menuName = "Scriptables/Grid/GridData")]
    public class GridDataSO : ScriptableObject
    {
        public GridRow[] GridRows;
        public List<GridBuilingBlock> GridBuildings;
    }
}
