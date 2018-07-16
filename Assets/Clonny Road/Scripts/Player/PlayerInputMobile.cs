using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInputMobile : PlayerInput
{
    private Command movementCommand = new MovementCommand();

    private float minSwipeDistance = 4.0f;

    private Vector2 touchStart = new Vector2();
    private bool initialized = false;

    public override List<Command> GetInput()
    {
        List<Command> inputCommands = new List<Command>();

        //Just for debug
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            initialized = true;
            touchStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(!initialized)
                return inputCommands;

            Vector2 touchEnd = Input.mousePosition;

            float x = touchEnd.x - touchStart.x;
            float y = touchEnd.y - touchStart.y;

            if (Mathf.Abs(y) > minSwipeDistance && Mathf.Abs(y) > Mathf.Abs(x))
            {
                //moves down
                if (y < 0)
                {
                    ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, 180, 0);
                    ((MovementCommand)movementCommand).MovementeVector = Vector3.back;
                    inputCommands.Add(movementCommand);
                    return inputCommands;
                }
                //moves up
                if (y > 0)
                {
                    ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, 0, 0);
                    ((MovementCommand)movementCommand).MovementeVector = Vector3.forward;
                    inputCommands.Add(movementCommand);
                    return inputCommands;
                }
            }

            //if the swhipe wasn't long enough we dont detect any movement at all
            if (Mathf.Abs(x) > minSwipeDistance && Mathf.Abs(x) > Mathf.Abs(y))
            {
                //moves left
                if (x < 0)
                {
                    ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, -90, 0);
                    ((MovementCommand)movementCommand).MovementeVector = Vector3.left;
                    inputCommands.Add(movementCommand);
                    return inputCommands;
                }
                //moves right
                if (x > 0)
                {
                    ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, 90, 0);
                    ((MovementCommand)movementCommand).MovementeVector = Vector3.right;
                    inputCommands.Add(movementCommand);
                    return inputCommands;
                }
            }
        }
#endif
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchEnd = touch.position;

                float x = touchEnd.x - touchStart.x;
                float y = touchEnd.y - touchStart.y;

                if (Mathf.Abs(y) > minSwipeDistance && Mathf.Abs(y) > Mathf.Abs(x))
                {
                    //moves down
                    if (y < 0)
                    {
                        ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, 180, 0);
                        ((MovementCommand)movementCommand).MovementeVector = Vector3.back;
                        inputCommands.Add(movementCommand);
                        return inputCommands;
                    }
                    //moves up
                    if (y > 0)
                    {
                        ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, 0, 0);
                        ((MovementCommand)movementCommand).MovementeVector = Vector3.forward;
                        inputCommands.Add(movementCommand);
                        return inputCommands;
                    }
                }
                if (Mathf.Abs(x) > minSwipeDistance && Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //moves left
                    if (x < 0)
                    {
                        ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, -90, 0);
                        ((MovementCommand)movementCommand).MovementeVector = Vector3.left;
                        inputCommands.Add(movementCommand);
                        return inputCommands;
                    }
                    //moves right
                    if (x > 0)
                    {
                        ((MovementCommand)movementCommand).Rotation = Quaternion.Euler(270, 90, 0);
                        ((MovementCommand)movementCommand).MovementeVector = Vector3.right;
                        inputCommands.Add(movementCommand);
                        return inputCommands;
                    }
                }
            }
        }

        return inputCommands;
    }
}