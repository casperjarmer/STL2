using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class UIRectObject : MonoBehaviour
{
    private RectTransform _rectangleReactTransform;
    private TMP_Text _text;
    private Image retangleImage;

    public void Awake()
    {
        _rectangleReactTransform = GetComponent<RectTransform>();
        _text = GetComponentInChildren<TMP_Text>();
        retangleImage = GetComponent<Image>();
    }

    public void SetRectTransform(Rect rect)
    {
        _rectangleReactTransform.anchoredPosition = new Vector2(rect.x, rect.y);
        _rectangleReactTransform.sizeDelta = new Vector2(rect.width, rect.height);
    }

    public void SetColor(Color color)
    {
        retangleImage.color = color;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public RectTransform GetRectTransform()
    {
        return _rectangleReactTransform;
    }



}
