using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _coins;
 public void UpdateCoins(int coins)
    {
        _coins.text = "Coins: " + coins;
    }
}
