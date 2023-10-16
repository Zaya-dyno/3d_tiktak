using System;
using System.Drawing;

public class State
{
    public int[,,] board;
    public bool changed;
    public int side = 4;

    public State()
    {
        board = new int[side, side, side];
        changed = false;
    }

    public int place_one((int, int, int) place)
    {
        return placement(1, place);
    }

    public int place_two((int, int, int) place)
    {
        return placement(2, place);
    }

    public int placement(int player, (int, int, int) place)
    {
        if (board[place.Item1, place.Item2, place.Item3] == 0)
        {
            board[place.Item1, place.Item2, place.Item3] = player;
            return 0;
        }
        return -1;
    }

    public int check_win()
    {
        //check straight lines
        for (int i = 0; i < 3; i++)
        {
            int[] place = new int[3];

            for (int j = 0; j < side; j++)
            {
                place[i % 3] = j;
                for (int z = 0; z < side; z++)
                {
                    place[(i + 1) % 3] = z;
                    place[(i + 2) % 3] = 0;
                    int player = board[place[0], place[1], place[2]];
                    if (player == 0)
                    {
                        continue;
                    }
                    for (int y = 1; y < side; y++)
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
        for (int j = 0; j < 2; j++)
        {
            for (int z = 0; z < 2; z++)
            {
                int[] loc;
                loc = new int[] { 0, (side - 1) * j, (side - 1) * z };
                int player = board[loc[0], loc[1], loc[2]];
                if (player == 0)
                {
                    continue;
                }
                loc[0] += 1;
                int add_1 = 1;
                int add_2 = 1;
                if (j == 1) { add_1 = -1; }
                if (z == 1) { add_1 = -1; }
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

