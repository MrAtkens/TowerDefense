using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Lifes;
    
    [SerializeField]
    private int startMoney;

    [SerializeField]
    private int startLifes;

    [SerializeField]
    private Text moneyShow;

    [SerializeField]
    private Text lifeShow;

    private void Start()
    {
        Money = startMoney; 
        Lifes = startLifes;   
        moneyShow.text = Money.ToString();
        lifeShow.text = Lifes.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        moneyShow.text = Money.ToString();
        lifeShow.text = Lifes.ToString();
    }

}
