using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
using System.Drawing;
using static UnityEditor.FilePathAttribute;

public class Placeholder : MonoBehaviour
{
    public Vector3 edge;
    public Vector3 center;
    public float ball_size;
    public Material[] materials_player = new Material[2]; // Player1, Player2
    public Material[] materials_place = new Material[5];
    private int[] layers_int;
    public (int, int, int) selected;
    public State state;
    public Material materials_line;
    public float radius_line;
    static public int side = 4;
    public int height;
    public int old_height;
    public float dis = 0.25F;
    static public int turn;
    static public int winner;
    static bool gameEnd;
    GameObject[,,] gameArea;
    Ray ray;
    RaycastHit hitdata;

    void create_layers()
    {
        layers_int = new int[side];
        for(int i = 0; i < side; i++)
        {
            layers_int[i] = LayerMask.NameToLayer("Ball_" + i.ToString());
        }
    }

    void create_balls()
    {
        gameArea = new GameObject[side, side, side];
        Vector3 location = edge + (ball_size / 2) * Vector3.one;
        for (int x = 0; x < side; x++)
        {
            for (int y = 0; y < side; y++)
            {
                for (int z = 0; z < side; z++)
                {
                    GameObject t = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    t.transform.parent = transform;
                    t.transform.localPosition = location + (ball_size + dis) * new Vector3(x, y, z);
                    t.transform.localScale = new Vector3(ball_size, ball_size, ball_size);
                    t.GetComponent<MeshRenderer>().material = materials_place[y];
                    t.layer = layers_int[y];
                    t.name = "Ball_" + x.ToString() + "_" + y.ToString() + "_" + z.ToString();
                    gameArea[x, y, z] = t;
                }
            }
        }
    }

    void create_lines()
    {
        Vector3 location;
        float height = (side * ball_size + (side - 1) * dis);

        for (int axis = 0; axis < 3; axis++)
        {
            Vector3[] vectors = new Vector3[] { Vector3.right, Vector3.up, Vector3.forward };
            location = vectors[axis % 3] * height / 2;
            location += (vectors[(axis + 1) % 3] + vectors[(axis + 2) % 3]) * (ball_size + dis / 2);
            for (int x = 0; x < side - 1; x += side - 2)
            {
                for (int z = 0; z < side - 1; z += side - 2)
                {
                    GameObject t = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    t.transform.parent = transform;
                    float scaleH = height;
                    if (axis == 1)
                    {
                        scaleH /= 2;
                    }
                    t.transform.localScale = (vectors[(axis + 1) % 3] + vectors[(axis + 2) % 3]) * radius_line + vectors[axis % 3] * scaleH;
                    t.transform.localPosition = location + (vectors[(axis + 1) % 3] * x + vectors[(axis + 2) % 3] * z) * (ball_size + dis);
                    t.GetComponent<MeshRenderer>().material = materials_line;
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameEnd = false;
        winner = 0;
        turn = 0;
        old_height = -1;
        state = new State();
        create_layers();
        create_balls();
        create_lines();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd)
        {
            return;
        }
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
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitdata, 1000, 1 << layers_int[height]) )
        {
            string hit = hitdata.transform.gameObject.name;
            selected = (hit[5] - 48, hit[7] - 48, hit[9] - 48);
        } else
        {
            selected = (-1, -1, -1);
        }
        if (Input.GetMouseButtonDown(0) && selected.Item1 != -1)
        {
            int ret = state.placement(turn + 1, selected);
            if(ret == 0)
            {
                gameArea[selected.Item1, selected.Item2, selected.Item3].GetComponent<MeshRenderer>().material = materials_player[turn];
                int temp = state.check_win();
                if (temp != 0 )
                {
                    winner = temp;
                    gameEnd = true;
                }
                turn = (turn + 1) % 2;
            }
        }
    }
}
