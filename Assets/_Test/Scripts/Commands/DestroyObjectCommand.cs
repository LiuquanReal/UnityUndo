using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectCommand : ICommand
{
    GameObject receiver;

    public DestroyObjectCommand(GameObject originObject)
    {
        this.receiver = originObject;
    }

    public void DestroyCommand()
    {
        GameObject.Destroy(receiver);
    }

    public void Execute()
    {
        receiver.SetActive(true);
    }
    public string CommandDescription()
    {
        return "消除了物体 " + receiver.name;
    }

}
