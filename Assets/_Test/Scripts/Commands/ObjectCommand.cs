using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class ObjectCommand:ICommand{
    UnityEngine.Object receiver;
    PropertyInfo[] propertyInfos;
    object[] values;

    public ObjectCommand(UnityEngine.Object receiver)
    {
        this.receiver = receiver;
        propertyInfos = receiver.GetType().GetProperties();
        values = new object[propertyInfos.Length];
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            try
            {
                values[i] = propertyInfos[i].GetValue(receiver, null);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                continue;
            }
        }
    }

    public void Execute()
    {
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            try
            {
                propertyInfos[i].SetValue(receiver, values[i], null);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                continue;
            }
        }
    }

    public void DestroyCommand()
    {
    }
}
