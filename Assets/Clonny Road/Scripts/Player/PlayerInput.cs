using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private Command movementCommand = new MovementCommand();

    public List<Command> GetInput()
    {
        List<Command> inputCommands = new List<Command>();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, 0, 0);
            ((MovementCommand)movementCommand).MovementeVector = Vector3.forward;
            inputCommands.Add(movementCommand);
            return inputCommands;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, 180, 0);
            ((MovementCommand)movementCommand).MovementeVector = Vector3.back;
            inputCommands.Add(movementCommand);
            return inputCommands;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, -90, 0);
            ((MovementCommand)movementCommand).MovementeVector = Vector3.left;
            inputCommands.Add(movementCommand);
            return inputCommands;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, 90, 0);
            ((MovementCommand)movementCommand).MovementeVector = Vector3.right;
            inputCommands.Add(movementCommand);
            return inputCommands;
        }

        return inputCommands;
    }
        

}
