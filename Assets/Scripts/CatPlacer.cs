using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CatPlacer : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    bool isPlacing = false;
    bool isPlaced = false;
    bool canPlace = false;
    [SerializeField] private GameObject catPrefab;
    private GameObject spawnedCat;

    private static readonly List<ARRaycastHit> rayHits = new();
    private void Update()
    {
        if (!raycastManager || !canPlace || isPlaced)
            return;

        if (Touchscreen.current == null)
            return;

        TouchControl primaryTouch = Touchscreen.current.primaryTouch;

        if (!primaryTouch.press.wasPressedThisFrame || isPlacing)
            return;

        isPlacing = true;

        Vector2 touchPosition = primaryTouch.position.ReadValue();

        TryPlaceCat(touchPosition);

        StartCoroutine(ResetPlacing());
    }

    private void TryPlaceCat(Vector2 touchPosition)
    {
        if (!raycastManager.Raycast(
                touchPosition,
                rayHits,
                TrackableType.PlaneWithinPolygon | TrackableType.FeaturePoint))
            return;

        Pose hitPose = rayHits[0].pose;
        if (spawnedCat == null)
        {
            spawnedCat = Instantiate(catPrefab, hitPose.position, hitPose.rotation);
            isPlaced = true;
        }
        else
        {
            spawnedCat.transform.position = hitPose.position;
            spawnedCat.transform.rotation = hitPose.rotation;
        }
    }

    public void SetCanPlace(bool value)
    {
        canPlace = value;
    }

    private IEnumerator ResetPlacing()
    {
        yield return new WaitForSeconds(0.25f);
        isPlacing = false;
    }
}
