using UnityEngine;

namespace ProjectCore.Collections
{
    [CreateAssetMenu(fileName = "GameObjetsArray", menuName = "Scriptables/Collections/GameObjetsArray")]
    public class SOGameObjectsArray : ScriptableObject
    {
        public GameObject[] Objects;
    }
}
