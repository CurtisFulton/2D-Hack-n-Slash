using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

public class MobController : BaseController
{
    const string
        SpriteAnimatorMovementXDeltaKey = "movementXDelta",
        SpriteAnimatorMovementYDeltaKey = "movementYDelta";

    protected GameObject SpriteGameObject { get; private set; }
    protected MovementComponent MovementComponent { get; private set; }
    
    [SerializeField]
    protected Sprite Sprite;

    [SerializeField]
    protected AnimatorController SpriteAnimatorController;

    protected Animator SpriteAnimator { get; set; }

    protected Vector3 spriteDirection;
    protected Direction SpriteDirection
    {
        get
        {
            return this.spriteDirection.ToDirection();
        }
    }

    protected virtual void Awake()
    {
        this.MovementComponent = this.gameObject.AddComponent<MovementComponent>();

        this.SpriteGameObject = new GameObject();
        this.SpriteGameObject.name = "Sprite";
        this.SpriteGameObject.transform.parent = this.transform;
        this.SpriteGameObject.transform.position = Vector3.zero;

        SpriteRenderer renderer = this.SpriteGameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = this.Sprite;

        this.SpriteAnimator = this.SpriteGameObject.AddComponent<Animator>();
        this.SpriteAnimator.runtimeAnimatorController = this.SpriteAnimatorController;
    }

    protected void SetSpriteDirection(Vector3 vector)
    {
        vector.Normalize();

        this.SpriteAnimator.SetFloat(SpriteAnimatorMovementXDeltaKey, vector.x);
        this.SpriteAnimator.SetFloat(SpriteAnimatorMovementYDeltaKey, vector.y);

        this.spriteDirection = vector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(this.transform.position, Vector3.one);
    }
}

