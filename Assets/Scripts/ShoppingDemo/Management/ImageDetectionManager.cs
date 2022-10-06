using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageDetectionManager : MonoBehaviour
{
    ARTrackedImageManager m_TrackedImageManager;
    ObjectSpawnManager m_SpawnManager;

    private void Awake()
    {
        //setup references
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        m_SpawnManager = GetComponentInChildren<ObjectSpawnManager>();
    }

    void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
          
            m_SpawnManager.Spawn(newImage);
        }

        //allowing multiple images to be tracked
        foreach (var updatedImage in eventArgs.updated)
        {
            if (updatedImage.trackingState != UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                updatedImage.gameObject.SetActive(false);
            }
            else
            {
                if (updatedImage.gameObject.activeSelf != true)
                    updatedImage.gameObject.SetActive(true);
            }
                
        }

        foreach (var removedImage in eventArgs.removed)
        {
            //scrren console renders debug messages on mobile screen when testing application
            ScreenConsole.Instance.Log($"Image removed {removedImage.name}");
        }

    }

}
