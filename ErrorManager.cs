```csharp
using System;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    public UIManager uiManager;
    public ThreadManager threadManager;

    void Start()
    {
        // Subscribe to the relevant events
        uiManager.OnError += HandleError;
        threadManager.OnError += HandleError;
    }

    private void HandleError(string errorMessage)
    {
        // Log the error message
        Debug.LogError(errorMessage);

        // Show an error dialog to the user
        // This is a simple example, you can replace it with your own UI logic
        // Make sure to run this on the main thread, as Unity's UI system is not thread-safe
        RunOnMainThread(() => {
            // Replace this with your own dialog UI
            // For example, you can use Unity's built-in UI system, or a third-party UI library
            Debug.Log("Error: " + errorMessage);
        });
    }

    private void RunOnMainThread(Action action)
    {
        StartCoroutine(RunOnMainThreadCoroutine(action));
    }

    private IEnumerator RunOnMainThreadCoroutine(Action action)
    {
        yield return null; // Wait until the next frame
        action();
    }

    void OnDestroy()
    {
        // Unsubscribe from the events when the object is destroyed
        // This is important to prevent memory leaks
        uiManager.OnError -= HandleError;
        threadManager.OnError -= HandleError;
    }
}
```
