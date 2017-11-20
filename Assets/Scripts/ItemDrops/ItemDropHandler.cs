using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropHandler : MonoBehaviour
{
    private GameObject _player;
    private GameObject _enemy;
    private GameObject _blastZone;
    private int _angle = 0;
    private int _rotateDirection = 1;
    private string _type;

    private Action<bool> _onDestroyCallback;
    private Action _onLanded;
    private Action<GameObject> _onCollisionCallback;

	void Start ()
    {
        _player = GameObject.Find("Player");
        _enemy = GameObject.Find("Enemy");
        _blastZone = GameObject.Find("BlastZone");
        
        gameObject.layer = 31;
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
    }

    void Update ()
    {
		transform.Rotate(new Vector3(0, 1, 0), _rotateDirection);
        _angle += _rotateDirection;
        if (_angle == 20 || _angle == -20)
            _rotateDirection *= -1;
    }

    public void SetType(string materialName)
    {
        _type = materialName;
        Material newMat = Resources.Load<Material>("Materials/ItemDrops/" + materialName);
        GetComponent<Renderer>().material = newMat;

        switch (materialName)
        {
            case "Speed":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                };
                break;
            case "Damage":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                };
                break;
            case "Jump":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                };
                break;
            case "Regeneration":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                };
                break;
            case "Resistance":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                };
                break;
            default:
                break;
        }
    }

    public void SetOnDestroyCallback(Action<bool> action)
    {
        _onDestroyCallback = action;
    }

    public void SetOnLandedCallback(Action action)
    {
        _onLanded = action;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject targetObject = collision.gameObject;

        if (targetObject != _player && targetObject != _enemy && targetObject != _blastZone)
        {
            // should be landed on ground
            _onLanded();
            return;
        }

        if (targetObject == _player || targetObject == _enemy)
            _onCollisionCallback(collision.gameObject);

        Destroy(gameObject);
        _onDestroyCallback(targetObject == _blastZone);
    }
}
