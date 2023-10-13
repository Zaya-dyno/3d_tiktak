using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Placeholder : MonoBehaviour
{
    public Vector3 edge;
    public Vector3 center;
    public float ball_size;
    public Material[] materials = new Material[3]; // Empty, Player1, Player2
    public int side = 4;
    public float dis = 0.25F;
    GameObject[,,] gameArea;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 location;
        gameArea = new GameObject[side,side,side];
        location = edge + (ball_size/2) * Vector3.one;
        center = edge + (ball_size / 2 * side + dis * (side - 1)) * Vector3.one;
        center = transform.TransformPoint(center);
        for (int x = 0; x < side; x++)
        {
            for(int y = 0; y < side; y++)
            {
                for(int z = 0; z < side; z++)
                {
                    GameObject t = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    t.transform.parent = transform;
                    t.transform.localPosition = location +(ball_size + dis) * new Vector3(x,y,z);
                    t.transform.localScale = new Vector3(ball_size, ball_size, ball_size);
                    t.GetComponent<MeshRenderer>().material = materials[0];
                    gameArea[x,y,z] = t;
                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
