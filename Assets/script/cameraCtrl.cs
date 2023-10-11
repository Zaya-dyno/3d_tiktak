using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCtrl : MonoBehaviour
{
    Dictionary<View, (Vector3, Vector3)> cameraPst = new Dictionary<View, (Vector3, Vector3)> { };
    public View view = View.Z;

    public enum View
    {
        Z,
        Y,
        X
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraPst[View.Z] = (new Vector3(-0.6F, 2.5F, -5F), Vector3.zero);
        cameraPst[View.X] = (new Vector3(), new Vector3());
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