using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectCommand : ICommand {

    UnityEngine.Object receiver;

    public CreateObjectCommand(UnityEngine.Object receiver)
    {
        this.receiver = receiver;
    }

    public void Execute()
    {
        UnityEngine.Object.Destroy(receiver);
    }

    public void DestroyCommand()
    {
    }

}
