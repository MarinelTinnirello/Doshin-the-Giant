using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTerrain : MonoBehaviour
{
    public Terrain terrain;             // terrain itself
    private TerrainData terrainData;    // terrain data to be grabbed and manipulated
    private int hmWidth;                // X-axis of the terrain
    private int hmHeight;               // Z-axis of the terrain
    private float[,] heights;           // how high an object will be on the X- Z- axes
    public float strength = 0.01f;      // strength of modification
    public int xBase = 4;               // x base of GetHeights()
    public int zBase = 4;               // z base of GetHeights()
    public int widthBase = 9;           // width of GetHeights()
    public int heightBase = 9;          // height of GetHeights()

    /**** Sets data to be modified ****/
    [System.Obsolete]
    void Start()
    {
        terrainData = terrain.terrainData;
        hmWidth = terrainData.heightmapWidth;
        hmHeight = terrainData.heightmapHeight;
        heights = terrainData.GetHeights(0, 0, hmWidth, hmHeight);
    }

    /**** Edits terrain at runtime, updates every frame ****/
    void Update()
    {
        RaycastHit hit;     // see what we're hitting
        // creates a ray from camera to current mouse position in game world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        /*** If left-click, then raise terrain ***/
        if (Input.GetMouseButton(0))
        {
            /*** If the raycast's ray registers a hit, grab data to raise terrain ***/
            if (Physics.Raycast(ray, out hit))
            {
                RaiseTerrain(hit.point);    // point in game world where we clicked
            }
        }
        /*** If right-click, then lower, terrain ***/
        if (Input.GetMouseButton(1))
        {
            /*** If the raycast's ray registers a hit, grab data to lower terrain ***/
            if (Physics.Raycast(ray, out hit))
            {
                LowerTerrain(hit.point);    // point in game world where we clicked
            }
        }
    }

    /**** Edits the terrain by raising it ****/
    private void RaiseTerrain(Vector3 point)
    {
        ///* for normalizing heighmap units into Unity units */
        //int mouseX = (int)((point.x / terrainData.size.x) * hmWidth);
        //int mouseZ = (int)((point.z / terrainData.size.z) * hmHeight);
        ///* can later be changed for things like brush size or type */
        //float[,] modifiedHeights = new float[1, 1];
        //float y = heights[mouseX, mouseZ];      // sets the height modified to the mouse coordinates

        //y += strength * Time.deltaTime;

        ///*** If the height exceeds the setting's height, set it equal to the setting's height ***/
        //if (y > terrainData.size.y)
        //{
        //    y = terrainData.size.y;
        //}

        ///* start modifiying based on where you click */
        //modifiedHeights[0, 0] = y;
        //heights[mouseX, mouseZ] = y;
        //terrainData.SetHeights(mouseX, mouseZ, modifiedHeights);

        /* for normalizing heighmap units into Unity units */
        int mouseX = (int)((point.x / terrainData.size.x) * hmWidth);
        int mouseZ = (int)((point.z / terrainData.size.z) * hmHeight);
        /* for newly created terrains */
        float[,] modifiedHeights = terrainData.GetHeights(mouseX - xBase, mouseZ - zBase, widthBase, heightBase);

        /*** creates parameters for the brushes
         * for the Z-axis, z is less than the height base, increment z
         * for the X-axis, x is less than the width base, increment x
         * then find the distance between the points, max distance, and amount to then set the brushes
        ***/
        for (int z = 0; z < heightBase; z++)
        {
            for (int x = 0; x < widthBase; x++)
            {
                float dis2Target = Mathf.Abs((float)z - zBase) + Mathf.Abs((float)x - xBase);
                float maxDis = ((widthBase + widthBase) / 2) - 1;
                float amount = dis2Target / maxDis;

                modifiedHeights[z, x] += strength * (1f - amount);
                heights[mouseX - xBase + x, mouseZ - zBase + z] += strength * (1f - amount);
            }
        }

        ///** Check that the strength of the brush is between a certain value **/
        //if (strength > 0.002)
        //{
        //    Debug.Log("Please choose a value below 0.001");
        //}
        //if (strength < 0.00001)
        //{
        //    Debug.Log("Please choose a value above 0.00001");
        //}

        ///** Checks that the x and zBase aren't larger or the same size as the width and heightBase **/
        //if (xBase >= widthBase || zBase >= widthBase)
        //{
        //    Debug.Log("Please choose a value less than the width");
        //}
        //if (xBase >= heightBase || zBase >= heightBase)
        //{
        //    Debug.Log("Please choose a value less than the height");
        //}

        /* modifies based on where you click */
        terrainData.SetHeights(mouseX - xBase, mouseZ - zBase, modifiedHeights);
    }

    /**** Edit the terrain by lowering it ****/
    private void LowerTerrain(Vector3 point)
    {
        ///* for normalizing heighmap units into Unity units */
        //int mouseX = (int)((point.x / terrainData.size.x) * hmWidth);
        //int mouseZ = (int)((point.z / terrainData.size.z) * hmHeight);
        ///* can later be changed for things like brush size or type */
        //float[,] modifiedHeights = new float[1, 1];
        //float y = heights[mouseX, mouseZ];      // sets the height modified to the mouse coordinates

        //y -= strength * Time.deltaTime;

        ///*** If the height is less than what the setting's height allows, set it equal to 0 ***/
        //if (y < 0)
        //{
        //    y = 0;
        //}

        ///* start modifiying based on where you click */
        //modifiedHeights[0, 0] = y;
        //heights[mouseX, mouseZ] = y;
        //terrainData.SetHeights(mouseX, mouseZ, modifiedHeights);

        /* for normalizing heighmap units into Unity units */
        int mouseX = (int)((point.x / terrainData.size.x) * hmWidth);
        int mouseZ = (int)((point.z / terrainData.size.z) * hmHeight);
        /* for newly created terrains */
        float[,] modifiedHeights = terrainData.GetHeights(mouseX + xBase, mouseZ + zBase, widthBase, heightBase);

        /*** creates parameters for the brushes
         * for the Z-axis, z is less than the height base, increment z
         * for the X-axis, x is less than the width base, increment x
         * then find the distance between the points, max distance, and amount to then set the brushes
        ***/
        for (int z = 0; z < heightBase; z++)
        {
            for (int x = 0; x < widthBase; x++)
            {
                float dis2Target = Mathf.Abs((float)z - zBase) + Mathf.Abs((float)x - xBase);
                float maxDis = ((widthBase + widthBase) / 2) - 1;
                float amount = dis2Target / maxDis;

                modifiedHeights[z, x] -= strength * (1f - amount);
                heights[mouseX - xBase + x, mouseZ - zBase + z] -= strength * (1f - amount);
            }
        }

        /* modifies based on where you click */
        terrainData.SetHeights(mouseX + xBase, mouseZ + zBase, modifiedHeights);
    }
}
