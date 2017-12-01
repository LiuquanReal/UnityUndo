using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Text console;
    public Button destroyObjBtn, destroyCompBtn,createObjBtn,addCompBtn;
    public GameObject cubePrefab;
    [HideInInspector]
    public GameObject currentObj;

    private void Start()
    {
        Operations.onRegisterCommand += OnOperationChange;
        Operations.onRemoveCommand += OnOperationChange;
        destroyCompBtn.onClick.AddListener(DestryComponent);
        destroyObjBtn.onClick.AddListener(DestroyGameObject);
        createObjBtn.onClick.AddListener(CreateObject);
        addCompBtn.onClick.AddListener(AddRigidbodyComponent);
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.tag == "SelectableObj")
                {
                    currentObj = hit.collider.gameObject;
                }
            }
            else
            {
                currentObj = null;
            }
        }
        currentObj = currentObj != null && currentObj.active ? currentObj : null;
        destroyCompBtn.interactable = currentObj != null && currentObj.GetComponent<BoxCollider>() != null;
        addCompBtn.interactable = currentObj != null && currentObj.GetComponent<AudioSource>() == null;
        destroyObjBtn.interactable = currentObj != null;
    }

    public void CreateObject()
    {
        GameObject cube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
        Operations.RegisterUndo(new CreateObjectCommand(cube));
    }

    public void DestryComponent()
    {
        BoxCollider bc = currentObj.GetComponent<BoxCollider>();
        Operations.RegisterUndo(new GameObjectCommand(currentObj));
        Destroy(bc);
    }

    public void DestroyGameObject()
    {
        DestroyObjectCommand dc = new DestroyObjectCommand(currentObj);
        Operations.RegisterUndo(dc);
        currentObj.SetActive(false);
    }

    public void AddRigidbodyComponent()
    {
        Operations.RegisterUndo(new GameObjectCommand(currentObj));
        currentObj.AddComponent<AudioSource>();
    }

    void OnOperationChange(ICommand command)
    {
        console.text = Operations.GetCommandsDescription();
    }
}
