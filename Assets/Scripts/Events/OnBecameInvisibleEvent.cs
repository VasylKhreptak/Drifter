using CBA.Events.Core;

namespace Events
{
    public class OnBecameInvisibleEvent : MonoEvent
    {
        private void OnBecameInvisible()
        {
            Invoke();
        }
    }
}
