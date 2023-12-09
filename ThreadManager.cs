```csharp
using System.Collections;
using System.Threading;
using UnityEngine;

public class ThreadManager : MonoBehaviour
{
    private Thread thread;
    private ImageGenerator imageGenerator;
    private string description;
    private Texture2D generatedImage;

    void Start()
    {
        imageGenerator = GetComponent<ImageGenerator>();
    }

    public void GenerateImageInThread(string description)
    {
        this.description = description;
        thread = new Thread(new ThreadStart(GenerateImage));
        thread.Start();
    }

    private void GenerateImage()
    {
        generatedImage = imageGenerator.GenerateImage(description);
    }

    void Update()
    {
        if (thread != null && !thread.IsAlive)
        {
            thread = null;
            OnImageGenerated();
        }
    }

    private void OnImageGenerated()
    {
        // Notify other components that the image has been generated
        // For example, you can use Unity's event system
        // You can also directly call methods on other components if they are accessible
    }

    void OnDestroy()
    {
        if (thread != null && thread.IsAlive)
        {
            thread.Abort();
        }
    }
}
```
