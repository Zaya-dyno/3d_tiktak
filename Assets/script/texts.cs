using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class texts : MonoBehaviour
{
    int old_turn;
    // Start is called before the first frame update

    public void setTurn()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = (Placeholder.turn + 1).ToString();
    }

    public void setWinner()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = gameObject.GetComponent<TextMeshProUGUI>().text
                                                          + (Placeholder.winner + 1).ToString();
    }

    void Start()
    {
        setTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if (old_turn != Placeholder.turn)
        {
            setTurn();
            old_turn = Placeholder.turn;
        }
        
    }
}
