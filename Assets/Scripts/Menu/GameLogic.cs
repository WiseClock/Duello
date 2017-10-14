using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private GameObject _menu;
    private GameObject _menuSelector;
    private GameObject _menuItemHolder;
    private GameObject _blur;

    private float _cameraAngleZ = 0;
    private float _cameraAngleZGrow = 0.01f;


    private int _selectedMenuItemIndex = 0;
    private bool _isChangingIndex = false;
    private float _changeStartTime = 0;

    private const int MENU_SELECTOR_OFFSET_X = 25;
    private const int MENU_SELECTOR_OFFSET_Y = 5;

    private const int CAMERA_ANGLE_Z_MAX = 1;

    void Start ()
	{
		_menu = GameObject.Find("MenuCanvas");
        _menuItemHolder = GameObject.Find("MenuSelection");
        _menuSelector = GameObject.Find("MenuSelector");
        _blur = GameObject.Find("Blur");
    }

	void Update ()
	{
	    float moveHorizontal = Input.GetAxis("Horizontal");
	    float moveVertical = Input.GetAxis("Vertical");
        int menuItemCount = _menuItemHolder.transform.childCount;

        Camera.main.transform.eulerAngles = new Vector3(moveVertical * 5, moveHorizontal * -5, _cameraAngleZ);
        _menu.transform.eulerAngles = new Vector3(moveVertical * 7, moveHorizontal * -7, 0);
	    int timeAfterSelecting = Mathf.RoundToInt((Time.fixedTime - _changeStartTime) * 1000);

        if (!_isChangingIndex)
	    {
	        if (moveVertical > 0)
	        {
	            _selectedMenuItemIndex = (_selectedMenuItemIndex + menuItemCount - 1) % menuItemCount;
	            _isChangingIndex = true;
	            _changeStartTime = Time.fixedTime;
	        }
	        else if (moveVertical < 0)
	        {
	            _selectedMenuItemIndex = (_selectedMenuItemIndex + menuItemCount + 1) % menuItemCount;
	            _isChangingIndex = true;
                _changeStartTime = Time.fixedTime;
            }
	    }
	    else if (moveVertical.Equals(0) || (timeAfterSelecting != 0 && timeAfterSelecting % 1000 == 0))
	    {
	        _isChangingIndex = false;
	    }

        if (_selectedMenuItemIndex >= 0 && _selectedMenuItemIndex < menuItemCount)
	    {
	        Transform menuItemTransform = _menuItemHolder.transform.GetChild(_selectedMenuItemIndex);
	        Vector3 menuItemPosition = menuItemTransform.localPosition + menuItemTransform.parent.localPosition;
            _menuSelector.transform.localPosition = menuItemPosition + new Vector3(MENU_SELECTOR_OFFSET_X, MENU_SELECTOR_OFFSET_Y, 0);
	    }
	}

    void FixedUpdate()
    {
        _cameraAngleZ += _cameraAngleZGrow;
        if (_cameraAngleZ >= CAMERA_ANGLE_Z_MAX || _cameraAngleZ <= -CAMERA_ANGLE_Z_MAX)
            _cameraAngleZGrow *= -1;
    }
}
