using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class InteractableSlot : MonoBehaviour
{
    private Assemble _assemble;
    private Mesh _mesh;

    private void Start()
    {
        _mesh = GetComponent<MeshCollider>().sharedMesh;
        _assemble = GetComponentInParent<Assemble>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") && other.TryGetComponent<MeshFilter>(out var filter))
        {
            if (_mesh == filter.sharedMesh)
            {
                other.GetComponent<ObjectManipulator>().enabled = false;
                other.gameObject.layer = 9;
                other.transform.SetParent(transform);
                other.transform.localPosition = Vector3.zero;
                other.transform.localRotation = Quaternion.identity;
                transform.GetComponent<MeshRenderer>().enabled = false;
                transform.GetComponent<MeshCollider>().enabled = false;
                _assemble.CurParts++;
            }
        }
    }
}
