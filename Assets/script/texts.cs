using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class texts : MonoBehaviour
{
    int old_turn;
    bool GameEnd;
    // Start is called before the first frame update

    public void setTurn()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = (Placeholder.turn + 1).ToString();
    }

    public void setWinner()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = gameObject.GetComponent<TextMeshProUGUI>().text
                                                          + "\n" + (Placeholder.winner).ToString();
    }

    void Start()
    {
        setTurn();
        GameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnd)
        {
            return;
        }
        if (old_turn != Placeholder.turn)
        {
            setTurn();
            old_turn = Placeholder.turn;
        }
        if (Placeholder.winner != 0)
        {
            setWinner();
            GameEnd = true;
        }
    }
}
