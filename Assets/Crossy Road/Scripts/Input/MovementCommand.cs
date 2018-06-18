using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCommand : Command {

    private Vector3 movementVector;
    public Vector3 MovementeVector
    {
        get
        {
            return movementVector;
        }
        set
        {
            movementVector = value;
        }
    }

    private Quaternion rotation;
    public Quaternion Rotation
    {
        get
        {
            return rotation;
        }
        set
        {
            rotation = value;
        }
    }
    public override void Execute(PlayerController character)
    {
        character.CheckIfIdle(rotation);
        character.MovementDirection = movementVector;
    }
}
