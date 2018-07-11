using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCommand : Command {


    /// <summary>
    /// The direction in which we are going to move
    /// </summary>
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

    /// <summary>
    /// the rotation in which we are going to look
    /// </summary>
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

    /// <summary>
    /// Execute the command 
    /// </summary>
    /// <param name="character">The character this command controls</param>
    public override void Execute(PlayerController character)
    {
        character.MovementDirection = movementVector;
        character.SetRotation(rotation);
        character.SetMove();
    }
}
