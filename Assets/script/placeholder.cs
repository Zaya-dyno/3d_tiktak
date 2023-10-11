using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeholder : MonoBehaviour
{
    public Vector3 center;
    public float ball_size;
    public Material material;
    public int side = 4;
    public float dis = 0.25F;
    GameObject[] balls;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 location;
        balls = new GameObject[side * side * side];
        location = center + new Vector3(ball_size / 2, ball_size / 2, - ball_size / 2);
        for (int i = 0; i < side; i++)
        {
            for(int j = 0; j < side; j++)
            {
                for(int z = 0; z < side; z++)
                {
                    GameObject t = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    t.transform.parent = transform;
                    t.transform.localPosition = location +(ball_size + dis) * new Vector3(i,j,-z);
                    t.transform.localScale = new Vector3(ball_size, ball_size, ball_size);
                    t.GetComponent<MeshRenderer>().material = material;
                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
