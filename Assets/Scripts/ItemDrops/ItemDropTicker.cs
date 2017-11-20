using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropTicker : MonoBehaviour
{
    public float RangeXMin = -8;
    public float RangeXMax = 8;
    public int MaxAllowedItems = 3;
    public int CheckIntervalSeconds = 3;
    public float Possibility = 0.3f;

    private int _currentItemCount = 0;
    private int _lastGeneratedTime = -1;

    private static readonly string[] ITEM_TYPES = { "Speed", "Damage", "Jump", "Regeneration", "Resistance" };

    void Start ()
	{
		
	}

	void FixedUpdate ()
	{
	    int timeInSeconds = (int)Time.realtimeSinceStartup;

        if (_currentItemCount < MaxAllowedItems && timeInSeconds > _lastGeneratedTime && timeInSeconds % CheckIntervalSeconds == 0 && Random.value < Possibility)
	    {
	        _currentItemCount++;
	        _lastGeneratedTime = timeInSeconds;
            float xPos = Random.Range(RangeXMin, RangeXMax);
            GameObject itemDrop = (GameObject)Instantiate(Resources.Load("Prefabs/ItemDrops/ItemDrop"));
            itemDrop.transform.position = new Vector3(xPos, 10, 0);
	        ItemDropHandler handler = itemDrop.GetComponent<ItemDropHandler>();
            handler.SetType(ITEM_TYPES[Mathf.FloorToInt((ITEM_TYPES.Length - 1) * Random.value)]);
            handler.SetOnDestroyCallback(OnItemDropDestroyCallback);
            Debug.Log("dropping item");
	    }
	}

    private void OnItemDropDestroyCallback()
    {
        _currentItemCount--;
    }
}
