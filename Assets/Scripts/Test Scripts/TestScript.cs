using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [ReadOnly] private int Test;

    private void Start()
    {

    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null) {
                var rb = hit.rigidbody;
                var eventComponent = rb.GetComponent<EventComponent>();
                eventComponent.CreateEvent<TestInstanceEvent>(new { test = "test" });
            }
        }
    }
}
