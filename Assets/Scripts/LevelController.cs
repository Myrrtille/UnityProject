using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController current;
    public static int coins = 0;
    public Text CoinsCounter;

    public Image blueCrystal;
    public Image greenCrystal;
    public Image redCrystal;

    public Text FruitsCounter;
    public int amountOfFruits = 0;
    private int collectedFruits = 0;

    void Awake()
    {
        current = this;
        if(blueCrystal != null) blueCrystal.enabled = false;
        if (greenCrystal != null) greenCrystal.enabled = false;
        if (redCrystal != null) redCrystal.enabled = false;
        if (FruitsCounter != null)
            FruitsCounter.text = "0/" + amountOfFruits;
        if (CoinsCounter != null)
        {
            int help = coins.ToString().Length;
            string label = "";
            for (int i = 0; i < 4 - help; i++)
                label += "0";
            label += coins;
            CoinsCounter.text = label;
        }
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
        int help = coins.ToString().Length;
        string label = "";
        for (int i = 0; i < 4-help; i++)
            label += "0";
        label += coins;
        CoinsCounter.text = label;
    }

    public void pickCrystal(int id)
    {
        if (id == 2)
            blueCrystal.enabled = true;
        else if (id == 3)
            greenCrystal.enabled = true;
        else if (id == 1)
            redCrystal.enabled = true;
    }

    public void addFruits(int id)
    {
        collectedFruits++;
        FruitsCounter.text = collectedFruits + "/" + amountOfFruits;
    }
}

//LevelController.current.getLifesCount();
