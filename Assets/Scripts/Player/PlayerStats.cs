using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int HP = 3;
    private int Lives = 5;
    private int Points = 0;

    public void loseHP()
    {
        if (HP > 0)
        {
            HP -= 1;
        }
    }

    public void loseLife()
    {
        if(Lives > 0)
        {
            Lives -= 1;
            HP = 3;
        }
    }

    public void gainHP()
    {
        if(HP <=2)
        {
            HP += 1;
        }
    }

    public void addPoints(int points)
    {
        Points = points;
    }

    public void restartPoints()
    {
        Points = 0;
    }

    public int getHP()
    {
        return HP;
    }

    public int getLives()
    {
        return Lives;
    }

    public int getPoints()
    {
        return Points;
    }

}
