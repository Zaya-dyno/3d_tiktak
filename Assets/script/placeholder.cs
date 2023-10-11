using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class placeholder : MonoBehaviour
{
    public Vector3 center;
    public float ball_size;
    public Material[] materials = new Material[3]; // Empty, Player1, Player2
    public int side = 4;
    public float dis = 0.25F;
    GameObject[,,] gameArea;
    GameObject[,,] highlights;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 location;
        gameArea = new GameObject[side,side,side];
        highlights = new GameObject[3,side,side]; // Z , Y , X
        location = center + new Vector3(ball_size / 2, ball_size / 2, ball_size / 2);
        for (int i = 0; i < side + 1; i++)
        {
            for(int j = 0; j < side + 1; j++)
            {
                for(int z = 0; z < side + 1; z++)
                {
                    int temp = Convert.ToInt16(i == side) + Convert.ToInt16(j == side) +
                               Convert.ToInt16(z == side);
                    if ( temp >= 2 )
                    {
                        continue;
                    }
                    GameObject t = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    t.transform.parent = transform;
                    t.transform.localPosition = location +(ball_size + dis) * new Vector3(i,j,z);
                    t.transform.localScale = new Vector3(ball_size, ball_size, ball_size);
                    t.GetComponent<MeshRenderer>().material = materials[0];
                    if ( temp == 1 )
                    {
                        int index;
                        if (i == side)
                        {
                            index = 0;
                        } else if (j == side)
                        {
                            index = 1;
                        } else
                        {
                            index = 2;
                        }
                    } else
                    {
                        gameArea[i,j,z] = t;
                    }

                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
