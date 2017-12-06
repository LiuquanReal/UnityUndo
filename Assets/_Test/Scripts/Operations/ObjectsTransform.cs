using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsTransform : MonoBehaviour {

    bool moving = false;

    public void StopMove()
    {
        moving = false;
    }

    public void Moving(Vector3 delta)
    {
        if (!moving)
        {
            moving = true;
            Operations.RegisterUndo(new GameObjectCommand(this.gameObject));
        }
        Vector3 pos = transform.position;
        pos += delta;
        transform.position = pos;
    }
}
