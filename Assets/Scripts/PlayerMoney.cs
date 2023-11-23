using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int moneyAmount;
    [SerializeField] private TextMeshPro moneyText;
    [SerializeField] private ARMenu menu;

    private void Start()
    {
        moneyText.text = $"Available money: {moneyAmount}";
    }

    public void ProcessBuy(int money)
    {
        CanBuy(money);
        moneyAmount -= money;
        moneyText.text = $"Available money: {moneyAmount}";
        menu.UpdateButtons();
    }

    public bool CanBuy(int price)
    {
        return moneyAmount >= price;
    }
}
