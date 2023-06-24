using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBreak : MonoBehaviour
{
    public int currentNumOfEnemiesTouching;
    public int maxEnemies = 7;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentNumOfEnemiesTouching++;
        if (currentNumOfEnemiesTouching >= maxEnemies)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        currentNumOfEnemiesTouching--;
    }
}
