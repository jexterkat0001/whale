using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class logicMoneyScript : MonoBehaviour
{
	public TextMeshProUGUI moneyText;

	private float money = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("addMoney")]
    public void addMoney(float moneyToAdd = 1f)
    {
    	money += moneyToAdd;
    	moneyText.text = Mathf.Round(money).ToString() + "$";
    }
}
