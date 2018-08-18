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
                Rigidbody2D rb = hit.rigidbody;
                EventComponent eventComponent = rb.GetComponent<EventComponent>();
                this.CreateEvent<TestInstanceEvent>(eventComponent, new TestInstanceEvent());
            }
        }
    }
}
