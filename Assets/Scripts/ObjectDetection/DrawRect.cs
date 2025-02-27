using System.Collections.Generic;
using UnityEngine;

public class DrawRect : MonoBehaviour
{
    [SerializeField] private GameObject _retanglePrefab;

    private List<UIRectObject> _rectObject = new();
    private List<int> _openIndices = new();

    public void CreateRectangle(Rect rect, Color color, string text)
    {
        if (_openIndices.Count == 0)
        {
            var newRect = Instantiate(_retanglePrefab, parent: transform).GetComponent<UIRectObject>();
            _rectObject.Add(newRect);
            _openIndices.Add(_rectObject.Count - 1);
        }

        int index = _openIndices[0];
        _openIndices.RemoveAt(0);

        UIRectObject rectObject = _rectObject[index];
        rectObject.SetRectTransform(rect);
        rectObject.SetColor(color);
        rectObject.SetText(text);
        rectObject.gameObject.SetActive(true);
    }

    public void ClearRects()
    {
        for (int i = 0; i < _rectObject.Count; i++)
        {
            _rectObject[i].gameObject.SetActive(false);
            _openIndices.Add(i);
        }
    }
}
