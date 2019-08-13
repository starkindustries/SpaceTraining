using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class BrickBreaker
{
    // Generates a row of block gameObjects. 
    // rowLength:       the number of blocks (including blank blocks) to generate
    // blocks:          the different block prefabs to choose from
    // skipPercentage:  the chance that a block should be blank/skipped
    // origin:          the x, y position of the topLeft block to use as an anchor for the rest of the blocks
    // parent:          the tilemap to use as the parent obect
    public static List<GameObject> GenerateRowOfBlocks(int rowLength, GameObject[] blocks, float skipPercentage, Vector3 origin, Tilemap parent)
    {
        List<int> blocksIndexList = GenerateRowOfIndices(rowLength, blocks.Length, skipPercentage);
        List<GameObject> blocksList = new List<GameObject>();

        for (int i = 0; i < blocksIndexList.Count; i++)
        {
            int index = blocksIndexList[i];
            if (index < 0)
            {
                // skip block if index is -1
            }
            else
            {
                Vector3 position = origin + new Vector3(x: 1, y: 0) * i;
                GameObject block = GameObject.Instantiate(original: blocks[index], position: position, rotation: Quaternion.identity, parent: parent.transform);
                blocksList.Add(item: block);
            }
        }

        return blocksList;
    }

    // Returns an array of integer indices. If the index is -1 then the the block is skipped.
    // Skip percentage is the chance that a block should be blank/skipped.
    // Skip percentage should be between 0 (0%, never skip) and 1 (100%, always skip)
    private static List<int> GenerateRowOfIndices(int rowLength, int blockCount, float skipPercentage)
    {
        List<int> indices = new List<int>();

        if (skipPercentage < 0 || skipPercentage > 1)
        {
            Debug.LogError("Incorrect skipPercentage value: " + skipPercentage + ". Value should be between 0 and 1 inclusive.");
            skipPercentage = 0;
        }

        for (int i = 0; i < rowLength; i++)
        {
            // Check if this block should be blank/skipped
            float shouldSkip = Random.Range(0f, 1f);
            if (shouldSkip < skipPercentage)
            {
                // Skip block
                indices.Add(item: -1);
            }
            else
            {
                // Add a random block
                int index = Random.Range(0, blockCount);
                indices.Add(item: index);
            }
        }
        return indices;
    }    
}
