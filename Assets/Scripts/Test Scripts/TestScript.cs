using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {

    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.CreateEvent(new TestEvent { Test = 1 });
        }
    }
}
