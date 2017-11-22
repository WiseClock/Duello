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

    public const int REGENERATION_AMOUNT = 20;
    public const float JUMP_BONUS_FACTOR = 0.3f;
    public const int JUMP_BONUS_LAST_SECONDS = 5;
    public const float RESISTANCE_FACTOR = 0.5f;
    public const int RESISTANCE_LAST_SECONDS = 5;
    public const float SPEED_BUFF_FACTOR = 0.5f;
    public const int SPEED_BUFF_LAST_SECONDS = 10;
    public const float ATTACK_BUFF_FACTOR = 0.3f;
    public const int ATTACK_BUFF_LAST_SECONDS = 5;

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
            case "Jump":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                    o.SendMessage("SetJumpBuff", new object[] { JUMP_BONUS_FACTOR, Time.realtimeSinceStartup + JUMP_BONUS_LAST_SECONDS });
                    AddEffect(o, JUMP_BONUS_LAST_SECONDS, new Color(0, 0, 255, 0.4f));
                };
                break;
            case "Regeneration":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                    o.SendMessage("RestoreHealth", REGENERATION_AMOUNT);
                };
                break;
            case "Resistance":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                    o.SendMessage("SetResistanceBuff", new object[] { RESISTANCE_FACTOR, Time.realtimeSinceStartup + RESISTANCE_LAST_SECONDS });
                    AddEffect(o, RESISTANCE_LAST_SECONDS, new Color(255, 255, 0, 0.4f));
                };
                break;
            case "Speed":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                    o.SendMessage("SetSpeedBuff", new object[] { SPEED_BUFF_FACTOR, Time.realtimeSinceStartup + SPEED_BUFF_LAST_SECONDS });
                    AddEffect(o, SPEED_BUFF_LAST_SECONDS, new Color(0, 255, 0, 0.4f));
                };
                break;
            case "Damage":
                _onCollisionCallback = o =>
                {
                    Debug.Log(o.name + " gets the " + _type + " buff!");
                    o.SendMessage("SetAttackBuff", new object[] { ATTACK_BUFF_FACTOR, Time.realtimeSinceStartup + ATTACK_BUFF_LAST_SECONDS });
                    AddEffect(o, ATTACK_BUFF_LAST_SECONDS, new Color(255, 0, 0, 0.4f));
                };
                break;
            default:
                break;
        }
    }

    private void AddEffect(GameObject owner, float duration, Color color)
    {
        GameObject effectParticle = (GameObject)Instantiate(Resources.Load("Prefabs/ItemDrops/EffectParticle"));
        var ps = effectParticle.GetComponent<ParticleSystem>().emission;
        ps.enabled = false;
        effectParticle.AddComponent<EffectTicker>();
        effectParticle.SendMessage("SetOwner", owner);
        ParticleSystem.MainModule settings = effectParticle.GetComponent<ParticleSystem>().main;
        settings.startColor = color;
        settings.duration = duration;
        ps.enabled = true;
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

        Destroy(gameObject);
        _onDestroyCallback(targetObject == _blastZone);

        if (targetObject == _player || targetObject == _enemy)
            _onCollisionCallback(collision.gameObject);
    }
}
