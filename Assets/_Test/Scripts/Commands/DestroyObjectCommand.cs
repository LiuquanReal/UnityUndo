using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectCommand : ICommand
{
    GameObject originObject;

    public DestroyObjectCommand(GameObject originObject)
    {
        this.originObject = originObject;
    }

    public void DestroyCommand()
    {
        GameObject.Destroy(originObject);
    }

    public void Execute()
    {
        originObject.SetActive(true);
    }

}
