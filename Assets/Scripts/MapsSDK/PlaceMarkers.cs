// Copyright 2022 Niantic, Inc. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using Niantic.Lightship.Maps.Core.Coordinates;
using Niantic.Lightship.Maps.MapLayers.Components;
using UnityEngine;

namespace Niantic.Lightship.Maps.Samples.GameSample
{
    /// <summary>
    /// This class checks for input for touching the map, both in tapping on resource
    /// features and for placing new structures
    ///
    /// ScreenPointToLatLong shows an example of converting a screen touch position to a
    /// coordinate on the map in (Latitude Longitude)
    /// </summary>
    internal class MapGameMapInteractions : MonoBehaviour
    {
        [SerializeField]
        private Camera _mapCamera;

        [SerializeField]
        private LightshipMapView _lightshipMapView;

        [SerializeField]
        private LayerGameObjectPlacement _markerSpawner;

        // man skal hive alle MarkerSpawner objekter ind i denne array i inspectoren
        public SculptureStats[] sculptures = new SculptureStats[1];

        private void Start()
        {
            PlaceMarkers();
        }

        private void Update()
        {
            var touchPosition = Vector3.zero;
            bool touchDetected = false;

            if (Input.touchCount == 1)
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    touchPosition = Input.touches[0].position;
                    touchDetected = true;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                touchPosition = Input.mousePosition;
                touchDetected = true;
            }
        }

        private void PlaceMarkers()
        {
            for (int i = 0; i < sculptures.Length; i++)
            {
                LatLng sculptureCoordinates = new LatLng(sculptures[i].latitude, sculptures[i].longitude);
                //sculptureLocations.Add(new Vector2((float)sculptureCoordinates.Latitude,(float)sculptureCoordinates.Longitude));
                
                _markerSpawner.PlaceInstance(sculptureCoordinates);
                GameObject marker = GameObject.Find("Marker");
                marker.name = sculptures[i].sculptureName;
                if (sculptures[i].isCollected)
                {
                    if (sculptures[i].sculpturePrefab != null)
                    {
                        GameObject sculpture = Instantiate(sculptures[i].sculpturePrefab, marker.transform.position, Quaternion.identity);
                        sculpture.transform.SetParent(marker.transform);
                        sculpture.transform.localPosition = new Vector3(0, -1, 0);
                        sculpture.transform.localRotation = Quaternion.identity;
                        sculpture.transform.localScale = Vector3.one*100;
                        Destroy(marker.transform.GetChild(1).gameObject);
                        marker.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;

                    }
                    else { 
                    marker.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
                    marker.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.green;
                    }
                }

            }
        }
    }
}
