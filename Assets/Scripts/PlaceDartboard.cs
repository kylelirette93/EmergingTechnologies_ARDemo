using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceDartboard : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    public bool canPlace = false;
    bool isPlacing = false;
    public bool HasPlaced { get { return hasPlaced; } }
    public bool hasPlaced = false;
    Vector3 offset = new Vector3(0.03f, 0f, 0f);

    private void Update()
    {
        if (!raycastManager || hasPlaced) return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began ||
            Input.GetMouseButtonDown(0) && !isPlacing)
        {
            isPlacing = true;

            if (Input.touchCount > 0)
            {
                PlaceObject(Input.GetTouch(0).position);
            }
            else
            {
                PlaceObject(Input.mousePosition);
            }
        }
    }

    void PlaceObject(Vector2 touchPosition)
    {
        if (!canPlace) return;

        var rayHits = new List<ARRaycastHit>();
        raycastManager.Raycast(touchPosition, rayHits, TrackableType.AllTypes);

        if (rayHits.Count > 0)
        {
            Vector3 hitPosePosition = rayHits[0].pose.position + offset;
            Quaternion hitPoseRotation = rayHits[0].pose.rotation;
            Instantiate(raycastManager.raycastPrefab, hitPosePosition, hitPoseRotation);
            hasPlaced = true;
        }
    }
}
