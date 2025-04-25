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

    public GameObject objectSpawner;
    public SculptureStats[] sculptures;

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
        SceneManager.sceneLoaded += FindObjectSpawner;
    }
    private void FindObjectSpawner(Scene scene, LoadSceneMode mode)
    {
        if( SceneManager.GetActiveScene().name == "Map")
        {
            objectSpawner = GameObject.Find("ObjectSpawner");
            sculptures = objectSpawner.GetComponent<MapGameMapInteractions>().sculptures;
        }
    }


    public void LoadObjectDetectionScene(GameObject sculpture)
    {
        
        for (int i = 0; i < sculptures.Length; i++)
        {
            if (sculptures[i].sculptureName == sculpture.transform.parent.name)
            {
                currentSculpture = sculptures[i];

            }
        }
        currentSculpture.isCollected = true;
    }
    
}
