using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawn2D : MonoBehaviour
{
    public Texture2D redDotTexture;

    public GameObject objectToSpawn;

    public float spawnDepth;
    public float spacing;
    public float groundPixelSize;


    // Start is called before the first frame update
    void Start()
    {
        SpawnGroundFromImage();

    }

    
    void Update()
    {
        
    }

    void SpawnGroundFromImage()
    {
        if(redDotTexture == null)
        {
            Debug.Log("No texture :( ");
            return;
        }

        bool[,] occupiedPixels = new bool[redDotTexture.width,redDotTexture.height]; 

        int groundCount = 0;

        for(float y = 0; y<redDotTexture.height; y += groundPixelSize)
        {
            for(float x = 0; x <redDotTexture.width; x += groundPixelSize)
            {
                if(CanSpawnGrounds(redDotTexture,Mathf.FloorToInt (x), Mathf.FloorToInt (y), occupiedPixels))
                {
                    MarkOccupied(Mathf.FloorToInt(x),Mathf.FloorToInt(y), occupiedPixels);

                    Vector3 spawnPostion = new Vector3(x * spacing, y * spacing, spawnDepth) + transform.position;
                    Instantiate(objectToSpawn, spawnPostion, Quaternion.identity);

                    groundCount++;

                    Debug.Log("Grounds Spawned in " + groundCount);
                }

                
            }
        }
    }

    bool CanSpawnGrounds(Texture2D image, int startX, int startY, bool[,] occupied)
    {
        int redPixelCount = 0;

        for (int y = 0; y < Mathf.CeilToInt(groundPixelSize); y ++)
        {
            for (int x = 0; x < Mathf.CeilToInt(groundPixelSize); x++)
            {

                int pixelX = startX + x;
                int pixelY = startY + y;

                if(pixelX >= image.width || pixelY >= image.height)
                {
                    continue;
                }

                if (occupied[pixelX, pixelY])
                {
                    return false;
                }

                Color pixelColour = image.GetPixel(pixelX, pixelY);
                if (IsRed(pixelColour))
                {
                    redPixelCount++;
                }
            }
        }

        return redPixelCount >= groundPixelSize;
    } 

    void MarkOccupied(int startX, int startY, bool[,] occupied)
    {
        for (int y = 0; y < Mathf.CeilToInt(groundPixelSize); y++)
        {
            for (int x = 0; x < Mathf.CeilToInt(groundPixelSize); x++)
            {

                int pixelX = startX + x;
                int pixelY = startY + y;

                if(pixelX >= occupied.GetLength(0) || pixelY >= occupied.GetLength(1))
                {
                    continue;
                }

                occupied[pixelX, pixelY] = true;

            }
        }
    }

    private bool IsRed(Color color)
    {
        return color.r > 0 && color.g < 1 && color.b < 1;
    }
}
