using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsTransform : MonoBehaviour {

    bool moving = false;

    public void StopMove()
    {
        moving = false;
    }

    public void Moving(float x)
    {
        if (!moving)
        {
            moving = true;
            Operations.RegisterUndo(new GameObjectCommand(this.gameObject));
        }
        Vector3 pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }
}
