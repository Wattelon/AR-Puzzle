using Microsoft.MixedReality.Toolkit.UI;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Assemble : MonoBehaviour
{
    [SerializeField] private ItemsConfig config;
    [SerializeField] private GameObject partPrefab;
    [SerializeField] private GameObject assemble;
    [SerializeField] private PlayerMoney money;
    [SerializeField] private int minParts;
    [SerializeField] private int maxParts;

    private int partPrice;
    private int targetParts;
    private int curParts;

    public int CurParts { get { return curParts; } set { curParts++; if (curParts == targetParts) CompleteAssemble(); } }

    private void Start()
    {
        foreach (var child in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if (child.CompareTag("Item")) Destroy(child.gameObject);
            else
            {
                child.enabled = true;
                child.GetComponent<MeshCollider>().enabled = true;
            }
        }

        transform.position = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        targetParts = Random.Range(minParts, maxParts);
        for (int i = 0; i < targetParts; i++)
        {
            var randItem = config.Items[Random.Range(0, config.Items.Count)];
            partPrice += randItem.Price;
            var randPrefab = randItem.Prefab;
            var randTransform = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
            var part = Instantiate(partPrefab, transform);
            part.transform.localPosition = randTransform;
            part.transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)));
            var partMesh = randPrefab.GetComponent<MeshFilter>().sharedMesh;
            part.GetComponent<MeshFilter>().mesh = partMesh;
            part.GetComponent<MeshCollider>().sharedMesh = partMesh;
        }
        targetParts = transform.childCount;
    }

    private void CompleteAssemble()
    {
        int earned = (int)(partPrice * Math.Pow(1.2, targetParts));
        FindObjectOfType<PlayerMoney>().ProcessBuy(-earned);
        Instantiate(assemble);
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<ObjectManipulator>().enabled = true;
    }
}
