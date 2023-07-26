using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PaintCanvas : MonoBehaviour
{
    [SerializeField] private Vector2Int _textureSize;
    public Vector2Int textureSize { get { return _textureSize; } }

    public Texture2D texture { get; private set; }
    private Renderer r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();

        texture = new Texture2D(_textureSize.x, _textureSize.y);

        r.material.mainTexture = texture;

        //TODO don't use FindFirstObject <- its slow
        FindFirstObjectByType<Terrain>().materialTemplate = r.material;

        //TestingDraw();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            byte[] textureBytes = texture.EncodeToPNG();
            File.WriteAllBytes("Canvas_texture.png", textureBytes);
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            // Read the byte array from the file
            byte[] textureBytes = File.ReadAllBytes("Canvas_texture.png");
            texture.LoadImage(textureBytes);
        }
        
    }
}
