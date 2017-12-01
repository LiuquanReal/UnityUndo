using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operations : MonoBehaviour {

    public delegate void OperationHandler(ICommand command);
    public static event OperationHandler onRegisterCommand;
    public static event OperationHandler onRemoveCommand;
    public static int maxCommandNumber = 4;
    public static int commandsNum
    {
        get
        {
            return commands.Count;
        }
    }

    static List<ICommand> commands = new List<ICommand>();

    public static void RegisterUndo(ICommand command)
    {
        if (commands.Count == maxCommandNumber)
        {
            commands[0].DestroyCommand();
            commands.RemoveAt(0);
        }
        commands.Add(command);
        if (onRegisterCommand != null)
        {
            onRegisterCommand(command);
        }
    }

    //public static void RecordObject(UnityEngine.Object obj)
    //{
    //    ObjectCommand objCommand = new ObjectCommand(obj);
    //    RegisterUndo(objCommand);
    //}

    public static void RegisterCreateObjectCommand(UnityEngine.Object newObject)
    {
        CreateObjectCommand createObjCommand = new CreateObjectCommand(newObject);
    }

    public static void Undo()
    {
        Debug.Log(commands.Count);
        ICommand c = commands[commands.Count - 1];
        commands[commands.Count - 1].Execute();
        commands.RemoveAt(commands.Count - 1);
        if (onRemoveCommand != null)
        {
            onRemoveCommand(c);
        }
    }

    public static string GetCommandsDescription()
    {
        string s = "";
        if (commands.Count == 0)
        {
            s = "操作数为0";
        }
        else
        {
            for (int i = 0; i < commands.Count; i++)
            {
                s += "第" + i + "步： " + commands[i].CommandDescription() + "\r\n";
            }
        }
        return s;
    }

}
