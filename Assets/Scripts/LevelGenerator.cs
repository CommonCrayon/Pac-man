using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Level01;

    [SerializeField] private GameObject OutsideCorner;
    [SerializeField] private GameObject OutsideWall;
    [SerializeField] private GameObject InsideCorner;
    [SerializeField] private GameObject InsideWall;
    [SerializeField] private GameObject NormalPellet;
    [SerializeField] private GameObject PowerPellet;
    [SerializeField] private GameObject TWall;

    private int[,] levelMap =
       {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
       };


    private Vector3 TLPos = new Vector3(-13.5f, 14, 0);

    private void Start()
    {
        // Destroying Original Level Map
        Destroy(Level01);

        // Get full level map 2d array
        int[,] fullLevelMap = CreateFullLevelMap(levelMap);

        // Iterate over array and place sprites
        for (int row = 0; row < fullLevelMap.GetLength(0); row++)
        {
            for (int column = 0; column < fullLevelMap.GetLength(1); column++)
            {
                int value = fullLevelMap[row, column];

                // Set Position
                Vector3 pos = new Vector3(column, -row, 0);
                pos += TLPos;

                // Check what is the value of sprites around
                int valueUp = (row - 1 >= 0) ? fullLevelMap[row - 1, column] : 0;
                int valueRight = (column + 1 < fullLevelMap.GetLength(1)) ? fullLevelMap[row, column + 1] : 0;
                int valueDown = (row + 1 < fullLevelMap.GetLength(0)) ? fullLevelMap[row + 1, column] : 0;
                int valueLeft = (column - 1 >= 0) ? fullLevelMap[row, column - 1] : 0;


                // Instantiate sprite for according value
                if (value == 1)
                {
                    if ((valueUp == 1 || valueUp == 2) && (valueRight == 1 || valueRight == 2))
                    {
                        Instantiate(OutsideCorner, pos, Quaternion.Euler(0, 0, 90));
                    }
                    else if ((valueUp == 1 || valueUp == 2) && (valueLeft == 1 || valueLeft == 2))
                    {
                        Instantiate(OutsideCorner, pos, Quaternion.Euler(0, 0, 180));
                    }
                    else if ((valueDown == 1 || valueDown == 2) && (valueLeft == 1 || valueLeft == 2))
                    {
                        Instantiate(OutsideCorner, pos, Quaternion.Euler(0, 0, 270));
                    }
                    else
                    {
                        Instantiate(OutsideCorner, pos, Quaternion.Euler(0, 0, 0));
                    }
                }

                else if (value == 2)
                {
                    if ((valueUp == 1 || valueUp == 2) && (valueDown == 1 || valueDown == 2))
                    {
                        Instantiate(OutsideWall, pos, Quaternion.Euler(0, 0, 0));
                    }
                    else
                    {
                        Instantiate(OutsideWall, pos, Quaternion.Euler(0, 0, 90));
                    }
                }

                else if (value == 3)
                {
                    if ((valueUp == 3 || valueUp == 4) && (valueRight == 3 || valueRight == 4))
                    {
                        Instantiate(InsideCorner, pos, Quaternion.Euler(0, 0, 90));
                    }
                    else if ((valueUp == 3 || valueUp == 4) && (valueLeft == 3 || valueLeft == 4))
                    {
                        Instantiate(InsideCorner, pos, Quaternion.Euler(0, 0, 180));
                    }
                    else if ((valueDown == 3 || valueDown == 4) && (valueLeft == 3 || valueLeft == 4))
                    {
                        Instantiate(InsideCorner, pos, Quaternion.Euler(0, 0, 270));
                    }
                    else
                    {
                        Instantiate(InsideCorner, pos, Quaternion.Euler(0, 0, 0));
                    }
                }

                else if (value == 4)
                {
                    if ((valueUp == 3 || valueUp == 4 || valueUp == 7) && (valueDown == 3 || valueDown == 4 || valueDown == 7))
                    {
                        Instantiate(InsideWall, pos, Quaternion.Euler(0, 0, 0));
                    }
                    else
                    {
                        Instantiate(InsideWall, pos, Quaternion.Euler(0, 0, 90));
                    }
                }

                else if (value == 5)
                {
                    Instantiate(NormalPellet, pos, Quaternion.identity);
                }

                else if (value == 6)
                {
                    Instantiate(PowerPellet, pos, Quaternion.identity);
                }

                else if (value == 7)
                {
                    if (valueDown == 0 || valueDown == 5 || valueDown == 6)
                    {
                        Instantiate(TWall, pos, Quaternion.Euler(0, 0, 90));
                    }
                    else if (valueRight == 0 || valueRight == 5 || valueRight == 6)
                    {
                        Instantiate(TWall, pos, Quaternion.Euler(0, 0, 180));
                    }
                    else if (valueUp == 0 || valueUp == 5 || valueUp == 6)
                    {
                        Instantiate(TWall, pos, Quaternion.Euler(0, 0, 270));
                    }
                    else
                    {
                        Instantiate(TWall, pos, Quaternion.Euler(0, 0, 0));
                    }
                }
            }
        }
    }



    private int[,] CreateFullLevelMap(int[,] array)
    {
        int rows = array.GetLength(0);
        int columns = array.GetLength(1);

        int[,] MergedArray = new int[rows * 2, columns * 2];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                MergedArray[i, j] = array[i, j]; // Top left
                MergedArray[i, j + columns] = array[i, columns - j - 1]; // Top right
                MergedArray[i + rows, j] = array[rows - i - 1, j]; // Bottom left
                MergedArray[i + rows, j + columns] = array[rows - i - 1, columns - j - 1]; // Bottom right
            }
        }

        // Removing the duplicate Row
        for (int j = 0; j < MergedArray.GetLength(1); j++)
        {
            for (int i = rows; i < MergedArray.GetLength(0) - 1; i++)
            {
                MergedArray[i, j] = MergedArray[i + 1, j];
            }
        }

        // Resize the array to exclude the last row
        int[,] TrimmedArray = new int[MergedArray.GetLength(0) - 1, MergedArray.GetLength(1)];
        for (int i = 0; i < TrimmedArray.GetLength(0); i++)
        {
            for (int j = 0; j < TrimmedArray.GetLength(1); j++)
            {
                TrimmedArray[i, j] = MergedArray[i, j];
            }
        }

        return TrimmedArray;
    }
}
