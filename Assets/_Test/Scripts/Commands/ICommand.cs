using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    string CommandDescription();
    /// <summary>
    /// 执行撤销
    /// </summary>
    void Execute();
    /// <summary>
    /// 销毁命令
    /// </summary>
    void DestroyCommand();
}

