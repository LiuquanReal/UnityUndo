using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour {
    public Button undo;
	// Use this for initialization
	void Start () {
        Operations.onRegisterCommand += CheckOperationNum;
        Operations.onRemoveCommand += CheckOperationNum;
        undo.onClick.AddListener(Undo);
	}

    void Undo()
    {
        Operations.Undo();
    }

    void CheckOperationNum(ICommand command)
    {
        if (Operations.commandsNum <= 0)
        {
            undo.interactable = false;
        }
        else
        {
            undo.interactable = true;
        }
    }
}
