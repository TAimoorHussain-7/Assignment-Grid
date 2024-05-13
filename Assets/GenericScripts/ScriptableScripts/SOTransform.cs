using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "SOTransform", menuName = "Scriptables/Components/Transforms")]
    public class SOTransform : ScriptableObject
    {
        public Transform Component { get; set; }
    }
}
