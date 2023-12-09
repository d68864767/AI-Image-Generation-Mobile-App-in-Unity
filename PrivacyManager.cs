```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyManager : MonoBehaviour
{
    public UIManager uiManager;
    public bool userConsent = false;

    void Start()
    {
        // Subscribe to the relevant events
        uiManager.OnSaveButtonClick += RequestUserConsent;
    }

    private void RequestUserConsent()
    {
        // Check if user consent has already been given
        if (!userConsent)
        {
            // Request user consent
            // This is a simple example, you can replace it with your own UI logic
            // Make sure to run this on the main thread, as Unity's UI system is not thread-safe
            RunOnMainThread(() => {
                // Replace this with your own dialog UI
                // For example, you can use Unity's built-in UI system, or a third-party UI library
                Debug.Log("Requesting user consent for saving image to device...");
                // Assume user gives consent
                userConsent = true;
            });
        }
    }

    private void RunOnMainThread(System.Action action)
    {
        StartCoroutine(RunOnMainThreadCoroutine(action));
    }

    private IEnumerator RunOnMainThreadCoroutine(System.Action action)
    {
        yield return null; // Wait until the next frame
        action();
    }

    void OnDestroy()
    {
        // Unsubscribe from the events when the object is destroyed
        // This is important to prevent memory leaks
        uiManager.OnSaveButtonClick -= RequestUserConsent;
    }
}
```
