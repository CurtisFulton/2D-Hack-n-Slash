using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventListener : MonoBehaviour {

    EventComponent eventComponent;

    private void Start()
    {
        this.eventComponent = this.GetEventComponent();

        TestInstanceEvent.RegisterListener(int.MaxValue, this.eventComponent, this.TestCallback);
    }

    private void TestCallback(TestInstanceEvent args)
    {
        Debug.Log("Event was created for " + this.eventComponent.name);
    }
}

public class TestInstanceEvent : Event<TestInstanceEvent>
{

}
