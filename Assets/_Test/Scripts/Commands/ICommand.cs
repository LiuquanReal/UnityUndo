using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    /// <summary>
    /// 执行撤销
    /// </summary>
    void Execute();
    /// <summary>
    /// 当储存的操作数已满时，执行销毁操作命令
    /// </summary>
    void DestroyCommand();
}

