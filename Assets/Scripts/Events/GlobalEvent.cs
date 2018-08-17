using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public abstract class GlobalEvent<T>
{
    public delegate void EventCallback(T args);

    private static SortedList<int, List<EventCallback>> AllListeners { get; set; } = new SortedList<int, List<EventCallback>>();
    private static string GenericTypeName { get; set; } = typeof(T).Name;

    public static EventVerbosity Verbosity { get; set; } = EventVerbosity.Level1;

    #region Add Listener

    /// <summary>
    /// Adds a listener to <typeparamref name="T"/>. Defaults to the lowest possible priority.
    /// </summary>
    /// <param name="callback">Function to call when the event is fired.</param>
    public static void RegisterListener(EventCallback callback) => RegisterListener(int.MaxValue, callback);

    /// <summary>
    /// Adds a listener to <typeparamref name="T"/>. 
    /// </summary>
    /// <param name="priority">Priority of the listener. A lower value means the callback will be called earlier.</param>
    /// <param name="callback">Function to call when the event is fired.</param>
    public static void RegisterListener(int priority, EventCallback callback)
    {
        if (callback == null) {
            LogError($"Callback for event '{GenericTypeName}' cannot be null.");
        }

        List<EventCallback> listeners;
        if (AllListeners.TryGetValue(priority, out listeners)) {
            listeners.Add(callback);
        } else {
            AllListeners.Add(priority, new List<EventCallback> { callback });
        }
    }

    #endregion

    #region Remove Listener

    /// <summary>
    /// Removes the listener from <typeparamref name="T"/>. Looks on all prioity levels
    /// </summary>
    /// <param name="callback">Function that was registered as a listener.</param>
    public static void RemoveListener(EventCallback callback)
    {
        foreach (var listenerGroup in AllListeners) {
            RemoveListener(listenerGroup.Key, callback);
        }
    }

    /// <summary>
    /// Removes the listener from <typeparamref name="T"/>.
    /// </summary>
    /// <param name="priority">Priority that the listener was registered under.</param>
    /// <param name="callback">Function that was registered as a listener.</param>
    public static void RemoveListener(int priority, EventCallback callback)
    {
        if (callback == null) {
            LogError($"Trying to remove a callback with null value from event type '{GenericTypeName}'.");
        }

        List<EventCallback> listeners;
        if (AllListeners.TryGetValue(priority, out listeners)) {
            listeners.Remove(callback);

            // Remove this priority if there are no more listeners
            if (listeners == null || listeners.Count == 0) {
                AllListeners.RemoveAt(priority);
            }
        } else {
            // Only log as a warning as this isn't technically an error, just something to keep an eye on as it shouldn't happen.
            Debug.LogWarning($"Trying to remove a callback from priority {priority}, which does not exist on {GenericTypeName}.");
        }
    }

    #endregion

    #region Create Event

    /// <summary>
    /// Sends an event of type <typeparamref name="T"/> to any listeners. Creates a default data object to send.
    /// </summary>
    /// <param name="sender">Object that is creating the event. Event will not be created if it is null.</param>
    public static void CreateEvent(object sender) => CreateEvent(sender, default(T));

    /// <summary>
    /// Sends an event of type <typeparamref name="T"/> to any listeners.
    /// </summary>
    /// <param name="sender">Object that is creating the event. Event will not be created if it is null.</param>
    /// <param name="eventArgs">Data object for the event. Contains any information about the event.</param>
    public static void CreateEvent(object sender, T eventArgs)
    {
        // Always log the event. Even if it was 'Invalid'
        LogEvent(sender, eventArgs);

        // We don't create the event if the sender is null
        if (sender == null) 
            return;
        
        // Loop over all the listener priorities and run the listeners
        foreach (var listenerGroup in AllListeners) {
            // Cache values so they don't create constant lookups
            List<EventCallback> callbacks = listenerGroup.Value;
            int callbackCount = callbacks.Count;

            // For loop is slightly faster than foreach
            for (int i = 0; i < callbackCount; i++) {
                callbacks[i]?.Invoke(eventArgs);
            }
        }
    }

    #endregion

    #region Event Logging 

    /// <summary>
    /// Helper method to log with a stack trace.
    /// </summary>
    /// <param name="error">Error to log.</param>
    /// <param name="withStackTrace">If true, the stack trace will be appended on a new line after the error.</param>
    private static void LogError(string error, bool withStackTrace = false)
    {
        // Probably not needed. Unity includes a stack trace in the log
        if (withStackTrace)
            error += Environment.NewLine + StackTraceUtility.ExtractStackTrace();

        Debug.LogError(error);
    }

    private static void LogEvent(object sender, T eventArgs)
    {
        if (Verbosity == EventVerbosity.None)
            return;

        // Log an error with stack trace if there was no event data
        if (sender == null) {
            LogError($"Trying to create an event of type '{GenericTypeName}' without a sender.");
            return;
        }

        // If there is no data don't display anything. If there is append that.
        string data = string.Empty;
        if (eventArgs != null)
            data = $"with data: { eventArgs?.ToString()}";
        

        Debug.Log($"{GenericTypeName} was raised by {sender.ToString()} {data}");
    }

    #endregion
}

public enum EventVerbosity
{
    None,
    Level1
}