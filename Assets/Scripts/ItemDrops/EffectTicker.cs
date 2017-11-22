using UnityEngine;

public class EffectTicker : MonoBehaviour
{
    private GameObject _owner;

    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
    }

    void SetOwner(GameObject go)
    {
        _owner = go;
    }

    void Update()
    {
        if (_owner != null)
            transform.position = _owner.transform.position;
    }
}
