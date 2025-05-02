using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectedText : MonoBehaviour
{
    SculptureStats[] sculptures;
    // Start is called before the first frame update
    void Start()
    {
        sculptures = Gamemanager.Instance.sculptures;
        int amount = 0;
        foreach (SculptureStats statue in sculptures)
        {
            if (statue.isCollected)
            {
                amount++;
            }
        }
        // Find the Text component in the GameObject named "CollectedText"
        GetComponent<TextMeshProUGUI>().text = "Collected: " + amount + "/" + sculptures.Length;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
