using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "CurrentGrid", menuName = "Scriptables/Grid/ProjectGrid")]
    public class ProjectGrid : ScriptableObject
    {
        public GridRow[] GridTiles;
    }
}
