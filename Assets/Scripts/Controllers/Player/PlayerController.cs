using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MobController {

    Camera playerCamera;

    protected override void Awake()
    {
        base.Awake();

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
        this.UpdateMovement();
        
    }

    private void UpdateMovement()
    {
        float
            movementXDelta = 0, 
            movementYDelta = 0;

        try
        {
            if (!Input.anyKey) return;

            if (Input.GetKey(MovementKeyBindings.Up))
            {
                movementYDelta = 1;
                this.MovementComponent.MoveUp();
            }

            if (Input.GetKey(MovementKeyBindings.Down))
            {
                movementYDelta = -1;

                this.MovementComponent.MoveDown();
            }

            if (Input.GetKey(MovementKeyBindings.Right))
            {
                movementXDelta = 1;

                this.MovementComponent.MoveRight();
            }

            if (Input.GetKey(MovementKeyBindings.Left))
            {
                movementXDelta = -1;
                this.MovementComponent.MoveLeft();
            }
        } finally
        {
            if (movementXDelta != 0 || movementYDelta != 0)
            {
                this.SetSpriteDirection(new Vector3(movementXDelta, movementYDelta));
                this.SpriteAnimator.SetBool("isWalking", true);

                if (Input.GetKeyDown(CombatKeyBindings.Lunge))
                {
                    // lunge forward in the direction  your facing
                    Vector3 currentDir = this.spriteDirection;
                    currentDir.Scale(new Vector3(5, 5));

                    this.MovementComponent.MoveTo(this.transform.position + currentDir, 0.5f);
                }
            }
            else
            {
                this.SpriteAnimator.SetBool("isWalking", false);
            }
        }
    }


}
