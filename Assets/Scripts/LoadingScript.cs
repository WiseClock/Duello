using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    private string[] _captions = { "A", "B", "CCC" };
    private string _nextSceneName = "";

    private Image _cover;
    private float _progressBarWidth;
    private RectTransform _progressBar;
    private Text _captionText;

    private State _currentState = State.FadeIn;
    private State _currentSubstate = State.FadeIn;
    private int _currentCaptionIndex = 0;
    private int _captionCountUp = 0;
    private float _progress = 0;

    void Start ()
    {
        _cover = GameObject.Find("Cover").GetComponent<Image>();
        _progressBarWidth = ((RectTransform)GameObject.Find("ProgressBg").transform).rect.width;
        _progressBar = (RectTransform)GameObject.Find("ProgressBar").transform;
        _captionText = GameObject.Find("CaptionText").GetComponent<Text>();

        _cover.color = new Color(0, 0, 0, 1);
        _captionText.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if (_currentState == State.FadeIn)
            return;

        // update progress bar
        if (_progress < 100)
	        _progress += 0.1f;
        float progressValue = _progressBarWidth * _progress / 100;
        _progressBar.sizeDelta = new Vector2(progressValue, _progressBar.sizeDelta.y);
    }

	void FixedUpdate ()
	{
	    if (_currentState == State.FadeIn)
	    {
	        float coverAlpha = _cover.color.a;
	        if (coverAlpha > 0)
	            coverAlpha -= 0.01f;
	        if (coverAlpha <= 0)
	            coverAlpha = 0;
	        _cover.color = new Color(0, 0, 0, coverAlpha);

            if (coverAlpha <= 0)
                _currentState = State.Captions;
	    }
        else if (_currentState == State.Captions)
	    {
            float captionAlpha = _captionText.color.a;

	        if (_currentSubstate == State.FadeIn && captionAlpha.Equals(0f))
	        {
	            _captionText.text = _captions[_currentCaptionIndex];
	        }

            if (_currentSubstate == State.FadeIn && captionAlpha < 1)
	        {
	            captionAlpha += 0.01f;
	            if (captionAlpha > 1)
	                captionAlpha = 1;
                _captionText.color = new Color(1, 1, 1, captionAlpha);
	        }
	        else if (_currentSubstate == State.FadeIn && captionAlpha >= 1)
	        {
	            if (_captionCountUp < 200)
	                _captionCountUp++;
	            else
	            {
	                _currentSubstate = State.FadeOut;
	            }
	        }
            else if (_currentSubstate == State.FadeOut && captionAlpha > 0)
	        {
                captionAlpha -= 0.015f;
                if (captionAlpha < 0)
                    captionAlpha = 0;
                _captionText.color = new Color(1, 1, 1, captionAlpha);
            }
            else if (_currentSubstate == State.FadeOut && captionAlpha <= 0)
            {
                _currentSubstate = State.FadeIn;
                _currentCaptionIndex++;
                _captionCountUp = 0;
                if (_currentCaptionIndex >= _captions.Length)
                    _currentState = State.FadeOut;
            }
        }
        else if (_currentState == State.FadeOut)
        {
            float coverAlpha = _cover.color.a;
            if (coverAlpha < 1)
                coverAlpha += 0.01f;
            if (coverAlpha >= 1)
                coverAlpha = 1;
            _cover.color = new Color(0, 0, 0, coverAlpha);
        }
    }

    private enum State
    {
        FadeIn,
        Captions,
        FadeOut,
    }
}
