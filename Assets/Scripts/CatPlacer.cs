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
    [SerializeField] private GameObject catPrefab;
    private GameObject spawnedCat;

    private bool isPlacing = false;
    private bool isPlaced = false;
    private bool canPlace = false;

    private static readonly List<ARRaycastHit> rayHits = new();
    private void Update()
    {
        if (!raycastManager || !canPlace || isPlaced)
            return;

        #region Get Input
        if (Touchscreen.current == null)
            return;

        TouchControl primaryTouch = Touchscreen.current.primaryTouch;

        if (!primaryTouch.press.wasPressedThisFrame || isPlacing)
            return;

        isPlacing = true;

        Vector2 touchPosition = primaryTouch.position.ReadValue();
        #endregion

        TryPlaceCat(touchPosition);

        StartCoroutine(ResetPlacing());
    }

    private void TryPlaceCat(Vector2 touchPosition)
    {
        // If nothing is detected return.
        if (!raycastManager.Raycast(
                touchPosition,
                rayHits,
                TrackableType.PlaneWithinPolygon | TrackableType.FeaturePoint))
            return;

        // Get the pose of the hit to move cat.
        Pose hitPose = rayHits[0].pose;
        if (spawnedCat == null)
        {
            // Spawn a cat if it doesn't exist.
            spawnedCat = Instantiate(catPrefab, hitPose.position, hitPose.rotation);
            isPlaced = true;
        }
        else
        {
            // If cat is placed, update position.
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
        // Reset with delay to avoid more than one placement per touch.
        yield return new WaitForSeconds(0.25f);
        isPlacing = false;
    }
}
