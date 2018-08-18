using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController {

    MovementComponent movementComponent;
    Camera playerCamera;
    Animator playerMobAnimator;
    GameObject playerMob;

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

        this.playerMob = this.transform.Find("Mob").gameObject;
        if(this.playerMob == null)
        {
            throw new System.Exception("The PlayerController must have a Mob GameObject in its children");
        }

        this.playerMobAnimator = this.playerMob.GetComponent<Animator>();
    }


    private void Update()
    {
        this.playerMobAnimator.SetBool("isWalkingDown", false);
        this.playerMobAnimator.SetBool("isWalkingUp", false);
        this.playerMobAnimator.SetBool("isWalkingLeft", false);
        this.playerMobAnimator.SetBool("isWalkingRight", false);

        float velX = 0;
        float velY = 0;
        if (Input.GetKey(MovementKeyBindings.Up))
        {
            velY = 1;
            this.movementComponent.MoveUp();
        }

        if (Input.GetKey(MovementKeyBindings.Down))
        {
            velY = -1;

            this.movementComponent.MoveDown();
            this.playerMobAnimator.SetBool("isWalkingDown", true);
        }

        if (Input.GetKey(MovementKeyBindings.Right))
        {
            velX = 1;

            this.movementComponent.MoveRight();
        }

        if(Input.GetKey(MovementKeyBindings.Left))
        {
            velX = -1;
            this.movementComponent.MoveLeft();
        }

        if(velX != 0 || velY != 0)
        {
            this.playerMobAnimator.SetFloat(nameof(velX), velX);
            this.playerMobAnimator.SetFloat(nameof(velY), velY);
        }
        
    }


}
