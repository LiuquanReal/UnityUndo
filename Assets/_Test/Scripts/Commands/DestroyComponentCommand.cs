using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class DestroyComponentCommand<T> : ICommand where T : Component
{
    GameObject receiver;
    Components<T> component;

    public DestroyComponentCommand(GameObject receiver,T component)
    {
        this.receiver = receiver;
        this.component = new Components<T>(component);
    }

    public void Execute()
    {
        component.SetInstance(receiver.AddComponent<T>());
    }

    public void DestroyCommand()
    {
    }
}

class Components<T> where T:Component
{
    PropertyInfo[] propertyInfos;
    object[] values;

    public Components(Component comp)
    {
        propertyInfos = comp.GetType().GetProperties();
        object[] vs = new object[propertyInfos.Length];
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            try
            {
                vs[i] = propertyInfos[i].GetValue(comp, null);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                continue;
            }
        }
        values = vs;
    }

    public void SetInstance(T mainObject)
    {
        mainObject = (T)Assembly.GetExecutingAssembly().CreateInstance(mainObject.GetType().ToString(), true, BindingFlags.Default, null, values, null, null);
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            try
            {
                propertyInfos[i].SetValue(mainObject, values[i], null);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                continue;
            }
        }
    }
}
