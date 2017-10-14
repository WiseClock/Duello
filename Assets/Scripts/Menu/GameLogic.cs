using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private GameObject _menu;
    private GameObject _menuSelection;

	void Start ()
	{
		_menu = GameObject.Find("MenuCanvas");
	}

	void Update ()
	{
	    float moveHorizontal = Input.GetAxis("Horizontal");
	    float moveVertical = Input.GetAxis("Vertical");
	    
	    Camera.main.transform.eulerAngles = new Vector3(moveVertical * 5, moveHorizontal * -5, 0);
        _menu.transform.eulerAngles = new Vector3(moveVertical * -5, moveHorizontal * 5, 0);
    }

    void FixedUpdate()
    {
        
    }
}
