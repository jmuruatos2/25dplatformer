using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _coins;
    [SerializeField]
    private GameObject _ladderText;
 public void UpdateCoins(int coins)
    {
        _coins.text = "Coins: " + coins;
    }

    public void LadderTextEnable(bool enabled)
    {
        _ladderText.SetActive(enabled);
    }
}
