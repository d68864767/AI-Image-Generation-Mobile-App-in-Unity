```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ImageGenerator imageGenerator;
    public InputField descriptionInputField;
    public Image outputImage;
    public Button generateButton;
    public Button saveButton;

    void Start()
    {
        generateButton.onClick.AddListener(OnGenerateButtonClick);
        saveButton.onClick.AddListener(OnSaveButtonClick);
    }

    private void OnGenerateButtonClick()
    {
        string description = descriptionInputField.text;
        if (string.IsNullOrEmpty(description))
        {
            Debug.LogError("Description cannot be empty");
            return;
        }

        Texture2D generatedImage = imageGenerator.GenerateImage(description);
        outputImage.sprite = Sprite.Create(generatedImage, new Rect(0.0f, 0.0f, generatedImage.width, generatedImage.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    private void OnSaveButtonClick()
    {
        if (outputImage.sprite == null)
        {
            Debug.LogError("No image to save");
            return;
        }

        StartCoroutine(SaveImage(outputImage.sprite.texture));
    }

    private IEnumerator SaveImage(Texture2D image)
    {
        yield return new WaitForEndOfFrame();

        byte[] bytes = image.EncodeToPNG();
        string path = System.IO.Path.Combine(Application.persistentDataPath, "GeneratedImage.png");
        System.IO.File.WriteAllBytes(path, bytes);

        Debug.Log("Image saved to " + path);
    }
}
```
