using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditInputField : MonoBehaviour
{
    public InputField inputField;
    Test test;
    ObjectsTransform currentBinding;
    void Awake()
    {
        inputField.onEndEdit.AddListener(delegate { EndEdit(); });
        test = FindObjectOfType<Test>();
    }
    private void Update()
    {
        if (test.currentObj)
        {
            currentBinding = test.currentObj.GetComponent<ObjectsTransform>();
            inputField.interactable = true;
            if (inputField.isFocused)
            {
                try
                {
                    currentBinding.Moving(float.Parse(inputField.text));
                }
                catch (System.Exception ex)
                {
                    Debug.Log(ex);
                    return;
                }
            }
            else
            {
                inputField.text = test.currentObj.transform.position.x.ToString();
                currentBinding.StopMove();
            }
        }
        else
        {
            inputField.interactable = false;
            currentBinding = null;
        }
    }
    void EndEdit()
    {
    }
}
