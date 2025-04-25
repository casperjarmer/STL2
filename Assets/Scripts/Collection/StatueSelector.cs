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

    // Constant placeholder string for the info text.
    private const string InfoPlaceholderText = "Information unavailable. Collect this statue to discover its history.";

    private void Start()
    {
        // Initialize the storage array for the original button images.
        if (statueButtons == null || statueButtons.Length <= 0)
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
            statueImage.sprite = placeholderSprite;
            return;
        }

        if (selectedStatue.image == null)
        {
            // Fall back to the placeholder if the image is missing.
            statueImage.sprite = placeholderSprite;
            return;
            
        }

        // Convert Texture2D to Sprite.
        Sprite statueSprite = Sprite.Create(
                selectedStatue.image,
                new Rect(0, 0, selectedStatue.image.width, selectedStatue.image.height),
                new Vector2(0.5f, 0.5f)
        );

        statueImage.sprite = statueSprite;
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
