using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Vector3Extension
{
    public static Direction ToDirection(this Vector3 vector)
    {
        // work out the direction the mob is facing by calculating the angle and moving counterclockwise in 45 degree pivots around a circle (8 rotations).
        // used with the Direction enum, its enum values represent each octant.
        float movementAngle = Mathf.Atan2(vector.y, vector.x);
        int octant = (int)Mathf.Round(8 * movementAngle / (2 * (float)Math.PI) + 8) % 8;

        return (Direction)octant;
    }
}

