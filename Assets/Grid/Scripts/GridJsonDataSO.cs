using UnityEngine;

namespace ProjectCore.Grid
{
    [CreateAssetMenu(fileName = "GridJson", menuName = "Scriptables/Grid/GridJson")]
    public class GridJsonDataSO : ScriptableObject
    {
        public GridJsonData CurrentJsonData = new GridJsonData();
    }
}
