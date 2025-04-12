using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatueManager : MonoBehaviour
{
    // Array of Scriptable Objects containing statue data.
    public SculptureStats[] statueDataList;

    // Reference to the UI Text component for displaying the statue's name and description.
    public TMP_Text infoText;

    // Reference to the UI Image component for displaying a larger version of the statue image.
    public Image statueImage;

    // Placeholder sprite to use when a statue is not collected.
    public Sprite placeholderSprite;

    // Array of Button components, where each Button contains an Image as a child that represents the icon.
    public Button[] statueButtons;

    // Private array to store each button's original sprite (the collected statue image).
    private Sprite[] originalButtonSprites;

    private void Start()
    {
        // Initialize the storage array.
        if (/*statueButtons != null && statueButtons.Length > 0*/ true)
        {
            originalButtonSprites = new Sprite[statueButtons.Length];
            Debug.Log("Statue buttons length: " + statueButtons.Length);

            // Loop through each button, assuming the button's child (e.g., index 0) holds the icon Image.
            for (int i = 0; i < statueButtons.Length; i++)
            {
                Debug.Log("Button ");
                // Try to get the Image component from the first child.
                if (statueButtons[i].transform.childCount > 0)
                {
                    Image childImage = statueButtons[i].transform.GetChild(0).GetComponent<Image>();
                    if (childImage != null)
                    {
                        originalButtonSprites[i] = childImage.sprite;
                    }
                    else
                    {
                        Debug.LogWarning("Button " + i + " child does not have an Image component.");
                    }
                }
                else
                {
                    Debug.LogWarning("Button " + i + " does not have a child GameObject for the icon.");
                }
            }
        }

        // Initially update all button images based on the current isCollected values.
        UpdateButtonImages();
    }

    /// <summary>
    /// Called when a statue button is pressed.
    /// The Button's OnClick() event should pass the corresponding statue index.
    /// </summary>
    /// <param name="index">Index of the statue in the statueDataList array.</param>
    public void OnStatueButtonPressed(int index)
    {
        // Validate that the index is within bounds.
        if (statueDataList == null || index < 0 || index >= statueDataList.Length)
        {
            Debug.LogError("Invalid statue index!");
            return;
        }

        // Retrieve the corresponding statue Scriptable Object.
        SculptureStats selectedStatue = statueDataList[index];

        // Update the info text with the statue's name and description.
        infoText.text = string.Format("{0}\n\n{1}",
                                      selectedStatue.sculptureName,
                                      selectedStatue.description);

        // Update the big image display:
        // Show the collected image if isCollected is true; otherwise, show the placeholder.
        if (selectedStatue.isCollected)
        {
            if (selectedStatue.image != null)
            {
                // Convert Texture2D to Sprite.
                Sprite statueSprite = Sprite.Create(selectedStatue.image,
                                                      new Rect(0, 0, selectedStatue.image.width, selectedStatue.image.height),
                                                      new Vector2(0.5f, 0.5f));
                statueImage.sprite = statueSprite;
            }
            else
            {
                // If no image is provided, fallback to placeholder.
                statueImage.sprite = placeholderSprite;
            }
        }
        else
        {
            statueImage.sprite = placeholderSprite;
        }
    }

    /// <summary>
    /// Updates the image on each button based on whether the corresponding statue is collected.
    /// If isCollected is true, the original button icon (from the child Image) is used.
    /// Otherwise, the button displays the placeholder sprite.
    /// </summary>
    public void UpdateButtonImages()
    {
        if (statueDataList == null || statueButtons == null) return;

        for (int i = 0; i < statueDataList.Length && i < statueButtons.Length; i++)
        {
            // Retrieve the Image component from the button's child.
            if (statueButtons[i].transform.childCount > 0)
            {
                Image childImage = statueButtons[i].transform.GetChild(0).GetComponent<Image>();
                if (childImage != null)
                {
                    // Set to original sprite if collected; else, set to placeholder.
                    childImage.sprite = statueDataList[i].isCollected ? originalButtonSprites[i] : placeholderSprite;
                }
            }
        }
    }
}
