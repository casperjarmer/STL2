using Niantic.Lightship.Maps.Samples.GameSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gamemanager : MonoBehaviour
{
    private static Gamemanager _instance;

    public static Gamemanager Instance { get { return _instance; } }

    
    public SculptureStats currentSculpture;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
    }

    public void LoadObjectDetectionScene()
    {
        SculptureStats[] sculptures = GetComponent<MapGameMapInteractions>().sculptures;
        GameObject player = GameObject.Find("Player");
        for (int i = 0; i < sculptures.Length; i++)
        {
            if (i == 0)
            {
                currentSculpture = sculptures[i];
                continue;
            }
            else if (Vector3.Distance(player.transform.position,new Vector3((float)sculptures[i].latitude, 0f, (float)sculptures[i].longitude)) <
                Vector3.Distance(player.transform.position, new Vector3((float)sculptures[i-1].latitude, 0f, (float)sculptures[i-1].longitude)))
            {
                currentSculpture = sculptures[i];
                continue;
            }
        }
    }
    public void GoToMap()
    {
        SceneManager.LoadScene("Map");
    }
}
