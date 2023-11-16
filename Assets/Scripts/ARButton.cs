using Microsoft.MixedReality.Toolkit.UI;
using System;
using TMPro;
using UnityEngine;

    public class ARButton : MonoBehaviour
    {
        public event Action OnButtonClicked;
        [SerializeField] private TextMeshPro title;
        [SerializeField] private Interactable interactable;
        private GameObject _prefab;

        public void Initialize(Item config)
        {
            title.text = config.Title;
            _prefab = config.Prefab;
            interactable.OnClick.AddListener(ProcessClick);
        }

        private void ProcessClick()
        {
            Instantiate(_prefab, transform.position, Quaternion.identity);
            OnButtonClicked?.Invoke();
        }
    }