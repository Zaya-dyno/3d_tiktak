using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCtrl : MonoBehaviour
{
    Dictionary<View, (Vector3, Vector3)> cameraPst = new Dictionary<View, (Vector3, Vector3)> { };
    public View view = View.Y;

    public enum View
    {
        X,
        Y,
        Z
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraPst[View.X] = (new Vector3(5F, 2.5F, -0.6F), new Vector3(0,-90F,0));
        cameraPst[View.Y] = (new Vector3(-1.4F,10F, -0.6F), new Vector3(90,180,-90));
        cameraPst[View.Z] = (new Vector3(-0.6F, 2.5F, 5F), new Vector3(0, 180F, 0));
    }

    // Update is called once per frame
    void Update()
    {
        (Vector3, Vector3) pos = cameraPst[view];
        transform.localPosition = pos.Item1;
        transform.localRotation = Quaternion.Euler(pos.Item2);
    }

    void FixedUpdate()
    {
    }
}