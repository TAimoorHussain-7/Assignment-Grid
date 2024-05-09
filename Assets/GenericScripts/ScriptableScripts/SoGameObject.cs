using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "SOGameObject", menuName = "Scriptables/Variables/GameObject")]

    public class SoGameObject : ScriptableObject
    {
        [SerializeField] GameObject NewObj, DefaultValue;
        public GameObject Obj { get => NewObj; set => NewObj = value; }

        [SerializeField] bool ResetDefault = true;

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
