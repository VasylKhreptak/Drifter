using CBA.Events.Core;

namespace Events
{
    public class OnBecameVisibleEvent : MonoEvent
    {
        private void OnBecameVisible()
        {
            Invoke();
        }
    }
}
