using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public Button destroyObjBtn, destroyCompBtn,createObjBtn;
    public GameObject cubePrefab;
    [HideInInspector]
    public GameObject currentObj;

    private void Start()
    {
        destroyCompBtn.onClick.AddListener(DestryComponent);
        destroyObjBtn.onClick.AddListener(DestroyGameObject);
        createObjBtn.onClick.AddListener(CreateObject);
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
                Debug.Log("+");
                currentObj = null;
            }
        }
        currentObj = currentObj != null && currentObj.active ? currentObj : null;
        destroyCompBtn.interactable = currentObj != null && currentObj.GetComponent<BoxCollider>() != null;
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
        DestroyComponentCommand<BoxCollider> dc = new DestroyComponentCommand<BoxCollider>(currentObj, bc);
        Operations.RegisterUndo(dc);
        Destroy(bc);
    }

    public void DestroyGameObject()
    {
        DestroyObjectCommand dc = new DestroyObjectCommand(currentObj);
        Operations.RegisterUndo(dc);
        currentObj.SetActive(false);
    }
}
