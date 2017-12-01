using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCommand : ICommand {

    GameObject receiver;
    Dictionary<Component,ComponentInfo<Component>> allComponents;

    public GameObjectCommand(GameObject receiver)
    {
        allComponents = new Dictionary<Component, ComponentInfo<Component>>();
        this.receiver = receiver;
        Component[] cs = receiver.GetComponents<Component>();
        foreach (var item in cs)
        {
            allComponents.Add(item,new ComponentInfo<Component>(item));
        }
    }

    public void DestroyCommand()
    {

    }

    public void Execute()
    {
        Component[] cs = receiver.GetComponents<Component>();
        Dictionary<Component, ComponentInfo<Component>> cis = allComponents;
        foreach (var item in cs)
        {
            if (!allComponents.ContainsKey(item))
            {
                UnityEngine.Object.Destroy(item);
            }
            else
            {
                cis[item].SetInstance(item);
                cis.Remove(item);
            }
        }
        foreach (var item in cis)
        {
            item.Value.SetInstance(receiver.AddComponent(item.Key.GetType()));
        }
    }
    public string CommandDescription()
    {
        return "记录物体 " + receiver.name+" 的状态";
    }

}
