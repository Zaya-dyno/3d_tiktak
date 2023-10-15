using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

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
    public float dis = 0.25F;
    GameObject[,,] gameArea;

    public class State
    {
        public int[,,] board;
        public bool changed;

        public State()
        {
            board = new int[side, side, side];
            changed = false;
        }

        public int place_one((int, int, int) place)
        {
            return placement(1,place);
        }

        public int place_two((int, int, int) place)
        {
            return placement(2, place);
        }

        private int placement(int player, (int,int,int) place)
        {
            if (board[place.Item1,place.Item2,place.Item3] == 0)
            {
                board[place.Item1, place.Item2, place.Item3] = player;
                return 0;
            } 
            return -1;
        }

        public int check_win()
        {
            //check straight lines
            for(int i = 0; i < 3; i++)
            {
                int[] place = new int[3];

                for(int j = 0; j < side; j++)
                {
                    place[i % 3] = j;
                    for(int z = 0; z < side; z++)
                    {
                        place[(i + 1) % 3] = z;
                        place[(i + 2) % 3] = 0;
                        int player = board[place[0], place[1], place[2]];
                        if (player == 0)
                        {
                            continue;
                        }
                        for(int y = 1; y < side; y++)
                        {
                            place[(i + 2) % 3] = y;
                            if (player != board[place[0], place[1], place[2]])
                            {
                                break;
                            }
                            if (y == side - 1)
                            {
                                return player;
                            }
                        }
                    }
                }
            }
            //check diagonals
            for(int j = 0; j < 2; j++)
            {
                for (int z = 0; z < 2; z++)
                {
                    int[] loc;
                    loc = new int[]{ 0, (side - 1) * j, (side - 1) * z};
                    int player = board[loc[0], loc[1], loc[2]];
                    if (player == 0)
                    {
                        continue;
                    }
                    loc[0] += 1;
                    int add_1 = 1;
                    int add_2 = 1;
                    if (j ==1) { add_1 = -1; }
                    if (z ==1) { add_1 = -1; }
                    loc[1] += add_1;
                    loc[2] += add_2;
                    for (int index = 1; index < side; index++)
                    {
                        if (player != board[loc[0], loc[1], loc[2]])
                        {
                            break;
                        }
                        loc[0] += 1;
                        loc[1] += add_1;
                        loc[2] += add_2;
                        if (index == side - 1)
                        {
                            return player;
                        }
                    }
                }
            }
            return 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
        
    }
}
