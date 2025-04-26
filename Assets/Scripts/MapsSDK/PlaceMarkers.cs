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

        //public List<Vector2> sculptureLocations = new List<Vector2>();

        //        [SerializeField]
        //        private FloatingText.FloatingText _floatingTextPrefab;
        /*
                [SerializeField]
                private LayerGameObjectPlacement _sawmillSpawner;

                [SerializeField]
                private LayerGameObjectPlacement _stoneMasonSpawner;

                [SerializeField]
                private LayerGameObjectPlacement _strongholdSpawner;
        */
        //        private MapGameState.StructureType _placingStructureType;
        private bool _placingStructure;

        public void StartPlacingStructure(/*MapGameState.StructureType structureType*/)
        {
            //_placingStructureType = structureType;
            //_placingStructure = true;
        }

        private void Start()
        {
            _placingStructure = true;
            PlaceMarkers();
            //StartCoroutine(PlaceMarkers());
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
            /*
            if (touchDetected)
            {
                if (_placingStructure == true)
                {
                    PlaceStructure(touchPosition);
                }
                else
                {
                    CheckForInteractableTouch(touchPosition);
                }
            }*/
        }

        private LatLng ScreenPointToLatLong(Vector3 screenPosition)
        {
            var clickRay = _mapCamera.ScreenPointToRay(screenPosition);
            var pointOnMap = clickRay.origin + clickRay.direction * (-clickRay.origin.y / clickRay.direction.y);
            return _lightshipMapView.SceneToLatLng(pointOnMap);
        }

        private void PlaceMarkers()
        {
            for (int i = 0; i < sculptures.Length; i++)
            {
                LatLng sculptureCoordinates = new LatLng(sculptures[i].latitude, sculptures[i].longitude);
                //sculptureLocations.Add(new Vector2((float)sculptureCoordinates.Latitude,(float)sculptureCoordinates.Longitude));
                
                _markerSpawner.PlaceInstance(sculptureCoordinates);
                GameObject.Find("Marker").name = sculptures[i].sculptureName;
            }
            /*foreach(GameObject child in buildingLayerGameObject.transform)
            {
                LatLng sculptureCoordinates = child.GetComponent<SculptureStatsSc>().coordinates;
                _markerSpawner.PlaceInstance(sculptureCoordinates); 
            }*/
            // Project the touch position onto the map and place a structure prefab there
            //var structureLatLng = ScreenPointToLatLong(touchPosition);
            //var statue1LatLng = new LatLng(55.39660748916102, 10.379028264689014); //Mads Hjem
            var statue1LatLng = new LatLng(55.36680088665288, 10.429245704987402);
            var statue2LatLng = new LatLng(55.366684144526296, 10.429179869438356);
            var statue3LatLng = new LatLng(55.366910475573114, 10.429215008732841);
            var statue4LatLng = new LatLng(55.36676846408718, 10.428824572127436);
            var statue5LatLng = new LatLng(55.36645981441671, 10.428503174433596);

            //var cameraForward = _mapCamera.transform.forward;
            //var forward = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
            //var rotation = Quaternion.LookRotation(forward);
            //_markerSpawner.PlaceInstance(statue1LatLng); //placing marker at statue1 position
            //_markerSpawner.PlaceInstance(statue2LatLng); //placing marker at statue2 position
            //_markerSpawner.PlaceInstance(statue3LatLng); //placing marker at statue3 position
            //_markerSpawner.PlaceInstance(statue4LatLng); //placing marker at statue4 position
            //_markerSpawner.PlaceInstance(statue5LatLng); //placing marker at statue5 position
            /*
            switch (_placingStructureType)
            {
                case MapGameState.StructureType.Sawmill:
                    _sawmillSpawner.PlaceInstance(structureLatLng, rotation);
                    break;

                case MapGameState.StructureType.StoneMason:
                    _stoneMasonSpawner.PlaceInstance(structureLatLng, rotation);
                    // Unlock stone resources on the map when StoneMason built
                    MapGameState.Instance.EnableResourceProduction(MapGameState.ResourceType.Stone, true);
                    break;

                case MapGameState.StructureType.Stronghold:
                    _strongholdSpawner.PlaceInstance(structureLatLng, rotation);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(_placingStructureType));
            }
            
            // Inform the inventory system so it can save this and alert other systems about the building
            MapGameState.Instance.StructureBuilt(structureLatLng, _placingStructureType);
            */

            // uncomment this line to stop placing structures after one is placed
            //_placingStructure = false;
        }

        private void CheckForInteractableTouch(Vector3 touchPosition)
        {
            var touchRay = _mapCamera.ScreenPointToRay(touchPosition);

            // raycast into scene and see if we hit a map feature
            if (!Physics.Raycast(touchRay, out var hitInfo))
            {
                return;
            }
            /*
            // check if the collider we hit is a feature
            var hitResourceItem = hitInfo.collider.GetComponent<MapGameResourceFeature>();
            if (hitResourceItem == null)
            {
                return;
            }

            // check if this resource has any units available to consume
            if (!hitResourceItem.ResourcesAvailable)
            {
                return;
            }

            // award the player resources for finding this map resource
            int amount = hitResourceItem.GainResources();
            MapGameState.Instance.AddResource(hitResourceItem.ResourceType, amount);

            // spawn an animated floating text to show resources being gained
            var floatingTextPosition = hitInfo.point + Vector3.up * 20.0f;
            var forward = floatingTextPosition - _mapCamera.transform.position;
            var rotation = Quaternion.LookRotation(forward, Vector3.up);
            var floatText = Instantiate(_floatingTextPrefab, floatingTextPosition, rotation);
            floatText.SetText($"+{amount} {hitResourceItem.ResourceType.ToString()}");*/
        }
    }
}
