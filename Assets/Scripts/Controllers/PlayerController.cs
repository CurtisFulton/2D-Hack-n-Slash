using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController {

    MovementComponent movementComponent;
    Camera playerCamera;

    #region CONTROLS

    

    #endregion  

    private void Awake()
    {
        this.movementComponent = this.gameObject.AddComponent<MovementComponent>();

        Transform cameraTransform = this.transform.Find("Camera");
        if(cameraTransform == null)
        {
            throw new System.Exception("The PlayerController does not have a Camera named 'Camera' in its children.");
        }

        this.playerCamera = cameraTransform.GetComponent<Camera>();

        if (this.playerCamera == null)
        {
            throw new System.Exception("There is no Camera Component on the 'Camera' object.");
        }
    }


    private void Update()
    {
        Input.GetKey()
    }


}
