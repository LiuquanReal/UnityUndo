using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 每个component脚本信息都可以用此类存储
/// </summary>
/// <typeparam name="T">可以指定具体的类</typeparam>
class ComponentInfo<T> where T : Component
{
    string typeName;
    PropertyInfo[] propertyInfos;
    object[] values;

    public ComponentInfo(Component comp)
    {
        typeName = comp.GetType().Name;
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
