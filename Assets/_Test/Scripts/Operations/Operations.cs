using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operations : MonoBehaviour {

    public delegate void OperationHandler();
    public static event OperationHandler onRegisterCommand;
    public static event OperationHandler onRemoveCommand;
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
        commands.Add(command);
        if (onRegisterCommand != null)
        {
            onRegisterCommand();
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
        commands[commands.Count - 1].Execute();
        commands.RemoveAt(commands.Count - 1);
        if (onRemoveCommand != null)
        {
            onRemoveCommand();
        }
    }


}
