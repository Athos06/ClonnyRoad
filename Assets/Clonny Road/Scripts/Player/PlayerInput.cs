using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayerInput
{
    public abstract List<Command> GetInput();
}


