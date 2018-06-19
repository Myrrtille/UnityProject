using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController current;
    public static int coins;

    void Awake()
    {
        current = this;
    }

    Vector3 startingPosition;
    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }
    public void onRabitDeath(HeroRabit rabit)
    {
        //При смерті кролика повертаємо на початкову позицію
        rabit.transform.position = this.startingPosition;
    }

    public void addCoins(int k)
    {
        coins += k;
    }

    public void addFruits(int id)
    {

    }
}

//LevelController.current.getLifesCount();
