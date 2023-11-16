using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ARMenu : MonoBehaviour
{
    [SerializeField] private ItemsConfig itemsConfig;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform root;
    [SerializeField] private GridObjectCollection gridObjectCollection;
    [SerializeField] private ClippingBox clippingBox;
    [SerializeField] private PlayerMoney playerMoney;

    private void Start()
    {
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        for (int i = 0; i < root.childCount; i++)
        {
            clippingBox.ClearRenderers();
            Destroy(root.GetChild(i).gameObject);
        }
        foreach (var itemsConfigItem in itemsConfig.Items)
        {
            if (!playerMoney.CanBuy(itemsConfigItem.Price)) continue;
            var button = Instantiate(buttonPrefab, root);
            foreach (var renderer in button.GetComponentsInChildren<Renderer>()) clippingBox.AddRenderer(renderer);
            if (button.TryGetComponent(out ARButton arButton))
            {
                arButton.Initialize(itemsConfigItem);
                arButton.OnButtonClicked += () => ProcessBuy(itemsConfigItem.Price);
            }
        }

        StartCoroutine(UpdateCollection());
    }

    private void ProcessBuy(int price)
    {
        playerMoney.ProcessBuy(price);
        UpdateButtons();
    }

    private IEnumerator UpdateCollection()
    {
        yield return new WaitForEndOfFrame();

        gridObjectCollection.UpdateCollection();
    }
}