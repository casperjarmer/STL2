//using Niantic.Lightship.AR.VpsCoverage;
using Niantic.Lightship.Maps;
using Niantic.Lightship.Maps.Core.Coordinates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{
    // Start is called before the first frame update

    LightshipMapView mapView; 
    void Awake() 
    { 
        
    }
    private void Start()
    {
        mapView = gameObject.GetComponent<LightshipMapView>();
        StartCoroutine("FindLocation");
        
    }
    IEnumerator FindLocation()
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            Debug.Log("Location not enabled on device or app does not have permission to access location");

        // Starts the location service.

        float desiredAccuracyInMeters = 5f;
        float updateDistanceInMeters = 5f;

        Input.location.Start(desiredAccuracyInMeters, updateDistanceInMeters);

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            yield break;
        }
        else
        {
            LatLng coords = new LatLng(Input.location.lastData.latitude, Input.location.lastData.longitude);
            mapView.SetViewableArea(coords, mapView.MapRadius);
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }

}
