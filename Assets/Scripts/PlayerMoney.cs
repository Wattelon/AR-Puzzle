using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int moneyAmount;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText.text = $"Available money: {moneyAmount}";
    }

    public void ProcessBuy(int money)
    {
        CanBuy(money);
        moneyAmount -= money;
        moneyText.text = $"Available money: {moneyAmount}";
    }

    public bool CanBuy(int price)
    {
        return moneyAmount >= price;
    }
}
