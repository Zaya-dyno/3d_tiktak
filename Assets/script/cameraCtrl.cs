using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class cameraCtrl : MonoBehaviour
{
    public float sensitivity;
    public bool change;
    public float upBoost;
    public Vector3 center = new Vector3(-2.7F, 2.3F, -2.7F);
    public float minH;
    public float maxH;
    public float minV;
    public float maxV;

    private Vector3 initialVectorH;
    private Vector3 initialVectorV;

    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 200;
        change = false;
        upBoost = 1.5F;
        transform.localPosition = new Vector3(6, 2.3F, -2.7F);
        transform.localRotation = Quaternion.Euler(Vector3.up * -90);
        initialVectorH = transform.position - center;
        initialVectorV = initialVectorH;
        initialVectorH.y = 0;
        initialVectorV.z = 0;
        minH = -90;
        minV = 0;
        maxH = 0;
        maxV = 90;
        Debug.Log(initialVectorV);
        Debug.Log(initialVectorH);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            change = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            change = false;
        }

        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        if (change)
        {
            float rotationH = rotateHorizontal * sensitivity * Time.deltaTime;
            float rotationV = - rotateVertical * upBoost * sensitivity * Time.deltaTime;

            Vector3 currentVectorH = transform.position - center;
            Vector3 currentVectorV = currentVectorH;
            currentVectorH.y = 0;
            currentVectorV.z = 0;
            float angleBetweenH = Vector3.Angle(initialVectorH, currentVectorH) * (Vector3.Cross(initialVectorH, currentVectorH).y > 0 ? 1 : -1);
            float angleBetweenV = Vector3.Angle(initialVectorV, currentVectorV) * (Vector3.Cross(initialVectorV, currentVectorV).z > 0 ? 1 : -1);
            rotationH = Mathf.Clamp(angleBetweenH + rotationH, minH, maxH);
            rotationV = Mathf.Clamp(angleBetweenV + rotationV, minV, maxV);
            rotationH -= angleBetweenH;
            rotationV -= angleBetweenV;
            transform.RotateAround(center, Vector3.up, rotationH);
            transform.RotateAround(center, transform.right, rotationV); 
        }
    }
}