using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "SOGameObject", menuName = "Scriptables/Variables/GameObject")]

    public class SoGameObject : ScriptableObject
    {
        [SerializeField] GameObject DefaultValue;
        [SerializeField] bool ResetDefault = true;

        public GameObject Obj { get; set; }


        private void OnEnable()
        {
            if (ResetDefault)
            {
                Obj = DefaultValue;
            }
        }

        public void DestroyObject()
        {
            if (Obj != null)
            {
                Destroy(Obj);
            }
        }
    }
}
