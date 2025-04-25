using Niantic.Lightship.Maps.Core.Coordinates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptureStatsSc : MonoBehaviour
{

    public SculptureStats stats;

    private string ID;

    private string sculptureName;

    private string description;
    private Texture2D image;

    public LatLng coordinates;

    // Start is called before the first frame update
    private void Awake()
    {
        ID = stats.ID;
        sculptureName = stats.sculptureName;
        description = stats.description;
        image = stats.image;
        coordinates = new LatLng(stats.latitude, stats.longitude);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayAudio()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && stats.audio != null)
        {
            audioSource.clip = stats.audio;
            audioSource.Play();
        }
    }
}
