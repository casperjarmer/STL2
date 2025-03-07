using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Niantic.Lightship.AR.ObjectDetection;

public class ObjectDetectionSample : MonoBehaviour
{
    [SerializeField] private float _probabilityThreshold = 0.1f; // Filter på hvor præcis det skal være som detected
    [SerializeField] private ARObjectDetectionManager _objectDetectionManager;
    [SerializeField] private DrawRect _drawRect;
    [SerializeField] private bool detectAllCategories = false; //To switch between detecting all categories or only sculptures

    private Canvas _canvas;

    private Color[] _colors = new Color[]
    {
        Color.green, //Color.red, Color.blue, Color.yellow, Color.magenta,
        //Color.cyan, Color.white, Color.black, Color.gray, Color.grey
    };

    private void Awake()
    {
        _canvas = Object.FindFirstObjectByType<Canvas>();
    }

    private void Start()
    {
        _objectDetectionManager.enabled = true;
        _objectDetectionManager.MetadataInitialized += ObjectDetectionManagerOnMetadataInitialized;
    }

    private void ObjectDetectionManagerOnMetadataInitialized(ARObjectDetectionModelEventArgs obj)
    {
        _objectDetectionManager.ObjectDetectionsUpdated += ObjectDetectionManagerOnObjectDetectionUpdated;
    }

    private void OnDestroy()
    {
        _objectDetectionManager.MetadataInitialized -= ObjectDetectionManagerOnMetadataInitialized;
        _objectDetectionManager.ObjectDetectionsUpdated -= ObjectDetectionManagerOnObjectDetectionUpdated;
    }

    private void ObjectDetectionManagerOnObjectDetectionUpdated(ARObjectDetectionsUpdatedEventArgs obj)
    {
        string resultString = " ";
        float _confidence = 0;
        string _name = " ";
        var result = obj.Results;

        if (result == null)
        {
            return;
        }

        _drawRect.ClearRects();

        for (int i = 0; i < result.Count; i++)
        {
            var detection = result[i];
            var categorizations = detection.GetConfidentCategorizations(_probabilityThreshold);

            if (categorizations.Count <= 0)
            {
                break;
            }

            categorizations.Sort((a, b) => b.Confidence.CompareTo(a.Confidence));

            var categoryToDisplay = categorizations[0];

            if (!detectAllCategories)
            {
                if (categorizations[0].CategoryName == "sculpture") // If the first category is a sculpture, we display that
                {
                    categoryToDisplay = categorizations[0];
                }
                else if (categorizations.Count > 1 && categorizations[1].CategoryName == "sculpture") // If the second category is a sculpture, we display that instead
                {
                    categoryToDisplay = categorizations[1];
                }
                else
                {
                    continue;
                }
            }

            _confidence = categoryToDisplay.Confidence;
            _name = categoryToDisplay.CategoryName;

            int h = Mathf.FloorToInt(_canvas.GetComponent<RectTransform>().rect.height);
            int w = Mathf.FloorToInt(_canvas.GetComponent<RectTransform>().rect.width);

            var rect = detection.CalculateRect(w, h, Screen.orientation);

            resultString = $"Detected: {_name} with confidence {_confidence:F2} \n";

            _drawRect.CreateRectangle(rect, _colors[i % _colors.Length], resultString);
        }
    }
}
