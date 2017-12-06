using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Axis { X,Y,Z}

public class EditInputField : MonoBehaviour
{
    public Axis axis;
    Test test;
    ObjectsTransform currentBinding;
    InputField inputField;
    void Awake()
    {
        inputField = GetComponent<InputField>();
        inputField.onValueChanged.AddListener(delegate { SetValue(); });
        inputField.onEndEdit.AddListener(delegate { EndEdit(); });
        test = FindObjectOfType<Test>();
        test.onSelectObject += SetSelectObject;
    }
    private void Update()
    {
        if (currentBinding && !inputField.isFocused)
        {
            switch (axis)
            {
                case Axis.X:
                    inputField.text = currentBinding.transform.position.x.ToString();
                    break;
                case Axis.Y:
                    inputField.text = currentBinding.transform.position.y.ToString();
                    break;
                case Axis.Z:
                    inputField.text = currentBinding.transform.position.z.ToString();
                    break;
                default:
                    break;
            }
        }
    }

    void SetSelectObject(GameObject gameObj)
    {
        if (gameObj)
        {
            currentBinding = gameObj.GetComponent<ObjectsTransform>();
        }
        else
        {
            currentBinding = null;
        }
        inputField.interactable = (currentBinding != null);
    }

    void SetValue()
    {
        if (inputField.isFocused)
        {
            try
            {
                Vector3 dir = Vector3.zero;
                switch (axis)
                {
                    case Axis.X:
                        dir = Vector3.right;
                        break;
                    case Axis.Y:
                        dir = Vector3.up;
                        break;
                    case Axis.Z:
                        dir = Vector3.forward;
                        break;
                    default:
                        break;
                }
                currentBinding.Moving(dir * float.Parse(inputField.text));
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex);
                return;
            }
        }
    }
    void EndEdit()
    {
        currentBinding.StopMove();
        inputField.text = currentBinding.transform.position.x.ToString();
    }
}
