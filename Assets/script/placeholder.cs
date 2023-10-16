using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
using System.Drawing;

public class Placeholder : MonoBehaviour
{
    public Vector3 edge;
    public Vector3 center;
    public float ball_size;
    public Material[] materials_player = new Material[2]; // Player1, Player2
    public Material[] materials_place = new Material[5];
    public State state;
    public Material materials_line;
    public float radius_line;
    static public int side = 4;
    public int height;
    public int old_height;
    public float dis = 0.25F;
    GameObject[,,] gameArea;

    // Start is called before the first frame update
    void Start()
    {
        old_height = -1;
        state = new State();
        Vector3 location;
        gameArea = new GameObject[side,side,side];
        location = edge + (ball_size/2) * Vector3.one;
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
                    t.GetComponent<MeshRenderer>().material = materials_place[y];
                    gameArea[x,y,z] = t;
                }
            }
        }

        float height = (side*ball_size+(side-1)*dis);

        for (int axis = 0; axis < 3; axis++)
        {
            Vector3[] vectors = new Vector3[] {Vector3.right, Vector3.up, Vector3.forward };
            location = vectors[axis%3] * height / 2;
            location += (vectors[(axis+1) % 3] + vectors[(axis + 2) % 3]) * (ball_size + dis / 2);
            for (int x = 0; x < side - 1; x+=side-2)
            {
                for (int z = 0; z < side - 1; z+=side-2)
                {
                    GameObject t = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    t.transform.parent = transform;
                    float scaleH = height;
                    if (axis == 1)
                    {
                        scaleH /= 2;
                    }
                    t.transform.localScale = (vectors[(axis + 1) % 3] + vectors[(axis + 2) % 3]) * radius_line + vectors[axis % 3] * scaleH;
                    t.transform.localPosition = location + (vectors[(axis + 1) % 3] * x + vectors[(axis + 2) % 3] * z) * (ball_size+dis);
                    t.GetComponent<MeshRenderer>().material = materials_line;
                }
            }
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (height != old_height)
        {
            for(int i = 0; i < side; i++)
            {
                float alpha;
                if (i == height)
                {
                    alpha = 100F/255F;
                } else
                {
                    alpha = 0;
                }
                UnityEngine.Color color = materials_place[i].color;
                color.a = alpha;
                materials_place[i].color = color;
            }
            old_height = height;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && height != side - 1)
        {
            height += 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && height != 0)
        {
            height -= 1;
        }
    }
}
