using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class StatueManager : MonoBehaviour
{
    // Array of Scriptable Objects containing statue data.
    public SculptureStats[] sculptures;

    // Reference to the UI Text component for displaying the statue's name and description.
    public TMP_Text infoText;

    // Reference to the UI Image component for displaying a larger version of the statue image.
    public Image sculptureImage;

    // Placeholder sprite to use when a statue is not collected.
    public Sprite placeholderSprite;

    public GameObject prefab;
    SculptureStats selectedStatue;

    // Array of Button components, where each Button contains an Image as a child that represents the icon.
    public Button[] statueButtons;

    // Private array to store each button's original sprite (the collected statue image).
    private Sprite[] originalButtonSprites;

    // Constant placeholder string for the info text.
    private const string InfoPlaceholderText = "Information unavailable. Collect this statue to discover its history.";

    private void Start()
    {
        sculptures = Gamemanager.Instance.sculptures;
        sculptureImage = GameObject.Find("Big Image").GetComponent<Image>();
        
        // Initialize the storage array for the original button images.
        /*if (statueButtons == null || statueButtons.Length <= 0)
        {
            return;
        }

        originalButtonSprites = new Sprite[statueButtons.Length];

        // Loop through each button, assuming the button's child (index 0) holds the icon Image.
        for (int i = 0; i < statueButtons.Length; i++)
        {
            // Try to get the Image component from the first child.
            if (statueButtons[i].transform.childCount == 0)
            {
                Debug.LogWarning("Button " + i + " does not have a child GameObject for the icon.");
                continue;
            }

            Image childImage = statueButtons[i].transform.GetChild(0).GetComponent<Image>();

            if (childImage == null)
            {
                Debug.LogWarning("Button " + i + " child does not have an Image component.");
                continue;
                
            }

            originalButtonSprites[i] = childImage.sprite;
        }
*/
        // Initially update all button images based on the current isCollected values.
        UpdateButtonImages();
    }



    
    /// <summary>
    /// Called when a statue button is pressed.
    /// The Button's OnClick() event should pass the corresponding statue index.
    /// </summary>
    /// <param name="index">Index of the statue in the statueDataList array.</param>
   
    public void OnStatueButtonPressed(GameObject button)
    {
        

        
        // Retrieve the corresponding statue Scriptable Object.
        for (int i = 0; i < sculptures.Length; i++)
        {
            if (sculptures[i].sculptureName == button.name)
            {
                selectedStatue = sculptures[i];
                Debug.Log("The statue is "+selectedStatue.sculptureName);
                break;
            }
            
        }

        // Conditionally update the info text.
        if (selectedStatue.isCollected)
        {
            // If collected, show the statue's name and description.
            infoText.text = string.Format(
                    "{0}\n\n{1}",
                    selectedStatue.sculptureName,
                    selectedStatue.description
            );
        }
        else
        {
            // If not collected, show the placeholder information.
            infoText.text = InfoPlaceholderText;
        }

        // Update the big image display:
        // Show the collected image if isCollected is true; otherwise, show the placeholder.
        if (!selectedStatue.isCollected)
        {
            sculptureImage.sprite = placeholderSprite;
            return;
        }

        if (selectedStatue.image == null)
        {
            // Fall back to the placeholder if the image is missing.
            sculptureImage.sprite = placeholderSprite;
            return;
            
        }


        sculptureImage.sprite = selectedStatue.image;
    }

    /// <summary>
    /// Updates the image on each button based on whether the corresponding statue is collected.
    /// If isCollected is true, the original button icon (from the child Image) is used.
    /// Otherwise, the button displays the placeholder sprite.
    /// </summary>
    public void UpdateButtonImages()
    {
        GameObject content = GameObject.Find("SculpturesContent");
        for (int i = 0; i < sculptures.Length; i++)
        {
            GameObject clone = Instantiate(prefab,content.transform);
            clone.GetComponent<Button>().onClick.AddListener(delegate { OnStatueButtonPressed(clone); });    
            clone.name = sculptures[i].sculptureName;
            if (sculptures[i].isCollected)
            {
                clone.transform.GetChild(0).GetComponent<Image>().sprite = sculptures[i].image;
                

            }
            else
            {
                clone.transform.GetChild(0).GetComponent<Image>().sprite = placeholderSprite;
            }
        }
    }
}
