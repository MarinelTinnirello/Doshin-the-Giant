using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public Terrain terrain;             // terrain itself
    private TerrainData terrainData;    // terrain data to be grabbed and manipulated

    /***** Captures the terrain data when the game begins *****/
    void Awake()
    {
        terrainData = terrain.terrainData;
    }

    /***** Calls EditTerrain() on the 1st frame *****/
    void Start()
    {
        EditTerrain();
    }

    /**** Edits the terrain
     * NOTE:    As of Unity 2019.3, TerrainData's heightmapWidth and Height functions have been deprecated
     *          They have been replaced with heighmapResolution
     *          If you wish to precede using the deprecated code, put "[System.Obsolete]" before the function
    ****/
    [System.Obsolete]
    private void EditTerrain()
    {

        int hmWidth = terrainData.heightmapWidth;            // X-axis of terrain, gets the width of the height map
        int hmHeight = terrainData.heightmapHeight;          // Z-axis of terrain, gets the height of the height map
        // an array of heights taking in the starting x and y, and how large the width and height will be
        float[,] heights = terrainData.GetHeights(0, 0, hmWidth, hmHeight);

        /*** Loops through each point in the array of heights ***/
        for (int z = 0; z < hmHeight; z++)
        {
            for (int i = 0; i < hmWidth; i++)
            {
                /* for wavy bumps for the terrain */
                float cos = Mathf.Cos(i);
                float sin = -Mathf.Sin(z);

                heights[i, z] = (cos - sin) / 250;         // keeps the bumps from being big spikes

            }
        }

        // takes in a starting x and y base, and a double array of heights (it will know where to end because of array)
        terrainData.SetHeights(0, 0, heights);
    }
}
