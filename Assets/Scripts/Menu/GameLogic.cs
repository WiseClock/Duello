using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private GameObject _menu;
    private GameObject _menuSelector;
    private GameObject _menuItemHolder;
    private GameObject _blur;

    private float _cameraAngleZ = 0;
    private float _cameraAngleZGrow = 0.01f;

    private bool _menuLoaded = false;

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

        Material blurMat = _blur.GetComponent<Image>().material;
        blurMat.SetFloat("_Size", 10);
        blurMat.SetColor("_Color", new Color(128, 0, 0));
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

        if (_menuLoaded) return;

        Material blurMat = _blur.GetComponent<Image>().material;
        float blurSize = blurMat.GetFloat("_Size");
        Color blurColor = blurMat.GetColor("_Color");
        if (blurSize > 0) blurSize -= 0.1f;
        if (blurSize < 0) blurSize = 0;
        float colorR = blurColor.r;
        float colorG = blurColor.g;
        float colorB = blurColor.b;
        if (colorR < 1) colorR += 0.01f;
        if (colorG < 1) colorG += 0.01f;
        if (colorB < 1) colorB += 0.01f;
        if (colorR > 1) colorR = 1;
        if (colorG > 1) colorG = 1;
        if (colorB > 1) colorB = 1;
        blurColor = new Color(colorR, colorG, colorB);

        blurMat.SetFloat("_Size", blurSize);
        blurMat.SetColor("_Color", blurColor);

        if (colorR.Equals(colorG) && colorG.Equals(colorB) && colorB.Equals(1) && blurSize.Equals(0))
            _menuLoaded = true;
    }
}
