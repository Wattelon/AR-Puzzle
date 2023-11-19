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
            var prefab = Instantiate(_prefab, transform.position, Quaternion.identity);
            var meshRenderer = prefab.GetComponent<MeshRenderer>();
            var material = new Material(meshRenderer.material);
            material.color = UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0.5f, 1);
            meshRenderer.material = material;
            OnButtonClicked?.Invoke();
        }
    }