using UnityEngine;

public static class MonobehaviourExtension
{
    #region Events


    /// <summary>
    /// Sends an event of type <typeparamref name="T"/> to any listeners. Creates a default data object to send.
    /// </summary>
    public static void CreateEvent<T>(this MonoBehaviour monoBehaviour) where T : Event<T>
    {
        Event<T>.CreateEvent(new MonobehaviourMetadata(monoBehaviour));
    }


    /// <summary>
    /// Sends an event of type <typeparamref name="T"/> to any listeners.
    /// </summary>
    /// <param name="eventArgs">Data object for the event. Contains any information about the event.</param>
    public static void CreateEvent<T>(this MonoBehaviour monoBehaviour, T eventArgs) where T : Event<T>
    {
        Event<T>.CreateEvent(new MonobehaviourMetadata(monoBehaviour), eventArgs);
    }

    public static void CreateEvent<T>(this MonoBehaviour monoBehaviour, EventComponent target, T eventArgs)
    {
        Event<T>.CreateEvent(new MonobehaviourMetadata(monoBehaviour), target, eventArgs);
    }

    /// <summary>
    /// Meta data struct to override the ToString method.
    /// </summary>
    private struct MonobehaviourMetadata
    {
        public MonoBehaviour Script { get; private set; }

        public MonobehaviourMetadata(MonoBehaviour script) { this.Script = script; }
        public override string ToString() => $"{this.Script.GetType().Name} on GameObject '{this.Script.gameObject.name}'";
    }

    #endregion

    public static EventComponent GetEventComponent(this MonoBehaviour monoBehaviour)
    {
        // Check the current Game Object for the event component
        var eventComponent = monoBehaviour.GetComponent<EventComponent>();
        if (eventComponent != null)
            return eventComponent;

        // Find an Event Component in the parent(s).
        return monoBehaviour.GetComponentInParent<EventComponent>();
    }
}