using UnityEngine;

namespace ProjectCore.Events
{
    public delegate void GameEventHandler();

    [CreateAssetMenu(fileName = "Event", menuName = "Scriptables/Events/WithOutParameter")]
    public class SOEvents : ScriptableObject
    {
        public event GameEventHandler Handler;

        public void InvokeEvent()
        {
            Handler?.Invoke();
        }
    }
}