using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour {

    private enum MovementDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    const float DefaultMovementSpeed = 1;

    public MovementComponent MoveUp(float? speedDelta) => this.Move(MovementDirection.Up, speedDelta);
    public MovementComponent MoveDown(float? speedDelta) => this.Move(MovementDirection.Down, speedDelta);
    public MovementComponent MoveRight(float? speedDelta) => this.Move(MovementDirection.Right, speedDelta);
    public MovementComponent MoveLeft(float? speedDelta) => this.Move(MovementDirection.Left, speedDelta);

    private MovementComponent Move(MovementDirection direction, float? speedDelta)
    {
        speedDelta = speedDelta ?? DefaultMovementSpeed;

        Vector3 positionDelta = Vector3.zero;
        switch(direction)
        {
            case MovementDirection.Up:
                positionDelta += new Vector3(0, 1, 0);
                break;
            case MovementDirection.Down:
                positionDelta += new Vector3(0, -1, 0);
                break;
            case MovementDirection.Right:
                positionDelta += new Vector3(1, 0, 0);
                break;
            case MovementDirection.Left:
                positionDelta += new Vector3(-1, 0, 0);
                break;
        }

        positionDelta *= speedDelta.Value * Time.deltaTime;

        this.gameObject.transform.position += positionDelta;

        return this;
    }
}
