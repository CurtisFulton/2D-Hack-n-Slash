using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventListener : MonoBehaviour {

    EventComponent eventComponent;

    private void Start()
    {
        this.eventComponent = this.GetEventComponent();

        this.eventComponent.RegisterListener<TestInstanceEvent>(args => {
            Debug.Log("I'm testing the instance event! Event called on " + this.transform.parent.name);
        });
    }
}

public class TestInstanceEvent : InstanceEvent
{

}
