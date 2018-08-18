using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The direction enumerated from East (right) going counter clockwise in 45 degree pivots.
/// </summary>
public enum Direction
{
    Right = 0,
    UpRight = 1,
    Up = 2,
    UpLeft = 3,
    Left = 4,
    DownLeft = 5,
    Down = 6,
    DownRight = 7
}

public class MovementComponent : MonoBehaviour {

    const float DefaultMovementSpeed = 1;
    Coroutine currentCoroutine;

    public MovementComponent MoveUp(float? speedDelta = null) => this.Move(Direction.Up, speedDelta);
    public MovementComponent MoveDown(float? speedDelta = null) => this.Move(Direction.Down, speedDelta);
    public MovementComponent MoveRight(float? speedDelta = null) => this.Move(Direction.Right, speedDelta);
    public MovementComponent MoveLeft(float? speedDelta = null) => this.Move(Direction.Left, speedDelta);

    public MovementComponent MoveTo(Vector3 destinationPosition, float secondsToComplete)
    {
        if(this.currentCoroutine != null)
        {
            this.StopCoroutine(this.currentCoroutine);
            this.currentCoroutine = null;
        }

        this.StartCoroutine(this.MoveToCoroutine(destinationPosition, secondsToComplete));

        return this;
    }

    private IEnumerator MoveToCoroutine(Vector3 destinationPosition, float secondsToComplete)
    {
        Vector3 currentPosition = this.transform.position;
        float
            currentDistanceToDestination = (currentPosition - destinationPosition).magnitude,
            movementDelta = currentDistanceToDestination / secondsToComplete * Time.deltaTime;

        do
        {
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, destinationPosition, movementDelta);

            this.transform.position = newPosition;
            currentPosition = newPosition;

            currentDistanceToDestination = (currentPosition - destinationPosition).magnitude;
            yield return null;
        } while (currentDistanceToDestination > 0.5);

        yield break;
    }

    private MovementComponent Move(Direction direction, float? speedDelta)
    {
        speedDelta = speedDelta ?? DefaultMovementSpeed;

        Vector3 positionDelta = Vector3.zero;
        switch(direction)
        {
            case Direction.Up:
                positionDelta += new Vector3(0, 1, 0);
                break;
            case Direction.Down:
                positionDelta += new Vector3(0, -1, 0);
                break;
            case Direction.Right:
                positionDelta += new Vector3(1, 0, 0);
                break;
            case Direction.Left:
                positionDelta += new Vector3(-1, 0, 0);
                break;
        }

        positionDelta *= speedDelta.Value * Time.deltaTime;

        this.gameObject.transform.position += positionDelta;

        return this;
    }

    
}
