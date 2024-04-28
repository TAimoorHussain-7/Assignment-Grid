using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "SOBool", menuName = "Scriptables/Variables/Bools")]
    public class SOBool : ScriptableObject
    {
        public bool Value { get; set; }

        [SerializeField] bool ResetDefault = true, DefaultValue;

        private void OnEnable()
        {
            if (ResetDefault)
            {
                Value = DefaultValue;
            }
        }
    }
}
