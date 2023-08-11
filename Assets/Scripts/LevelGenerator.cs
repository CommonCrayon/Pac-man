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


    private Vector3 StartingPosition; 


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



    private void Start()
    {
        // Destroying Original Level Map
        Destroy(Level01);


        StartingPosition = new Vector3((1.5f - levelMap.GetLength(0)), levelMap.GetLength(1), 0);
        // Get full level map 2d array
        int[,] fullLevelMap = CreateFullLevelMap(levelMap);



        // Iterate over array and place sprites
        for (int row = 0; row < fullLevelMap.GetLength(0); row++)
        {
            for (int column = 0; column < fullLevelMap.GetLength(1); column++)
            {
                int spriteValue = fullLevelMap[row, column];



                // Set Position
                Vector3 pos = new Vector3(column, -row, 0);
                pos += StartingPosition;



                // Check what is the value of sprites around
                int spriteUp = (row - 1 >= 0) ? fullLevelMap[row - 1, column] : 0;
                int spriteRight = (column + 1 < fullLevelMap.GetLength(1)) ? fullLevelMap[row, column + 1] : 0;
                int spriteDown = (row + 1 < fullLevelMap.GetLength(0)) ? fullLevelMap[row + 1, column] : 0;
                int spriteLeft = (column - 1 >= 0) ? fullLevelMap[row, column - 1] : 0;



                // Instantiate sprite for according value
                if (spriteValue == 1)
                {
                    if ((spriteUp == 1 || spriteUp == 2) && (spriteRight == 1 || spriteRight == 2))
                    {
                        Instantiate(OutsideCorner, pos, Quaternion.Euler(0, 0, 90));
                    }
                    else if ((spriteUp == 1 || spriteUp == 2) && (spriteLeft == 1 || spriteLeft == 2))
                    {
                        Instantiate(OutsideCorner, pos, Quaternion.Euler(0, 0, 180));
                    }
                    else if ((spriteDown == 1 || spriteDown == 2) && (spriteLeft == 1 || spriteLeft == 2))
                    {
                        Instantiate(OutsideCorner, pos, Quaternion.Euler(0, 0, 270));
                    }
                    else
                    {
                        Instantiate(OutsideCorner, pos, Quaternion.Euler(0, 0, 0));
                    }
                }

                else if (spriteValue == 2)
                {
                    if ((spriteUp == 1 || spriteUp == 2) && (spriteDown == 1 || spriteDown == 2))
                    {
                        Instantiate(OutsideWall, pos, Quaternion.Euler(0, 0, 0));
                    }
                    else
                    {
                        Instantiate(OutsideWall, pos, Quaternion.Euler(0, 0, 90));
                    }
                }

                else if (spriteValue == 3)
                {
                    if ((spriteUp == 3 || spriteUp == 4 || spriteUp == 7) && (spriteRight == 3 || spriteRight == 4 || spriteRight == 7))
                    {
                        Instantiate(InsideCorner, pos, Quaternion.Euler(0, 0, 90));
                    }
                    else if ((spriteUp == 3 || spriteUp == 4 || spriteUp == 7) && (spriteLeft == 3 || spriteLeft == 4 || spriteLeft == 7))
                    {
                        Instantiate(InsideCorner, pos, Quaternion.Euler(0, 0, 180));
                    }
                    else if ((spriteDown == 3 || spriteDown == 4 || spriteDown == 7) && (spriteLeft == 3 || spriteLeft == 4 || spriteLeft == 7))
                    {
                        Instantiate(InsideCorner, pos, Quaternion.Euler(0, 0, 270));
                    }
                    else
                    {
                        Instantiate(InsideCorner, pos, Quaternion.Euler(0, 0, 0));
                    }
                }

                else if (spriteValue == 4)
                {
                    if ((spriteUp == 3 || spriteUp == 4 || spriteUp == 7) && (spriteDown == 3 || spriteDown == 4 || spriteDown == 7))
                    {
                        Instantiate(InsideWall, pos, Quaternion.Euler(0, 0, 0));
                    }
                    else
                    {
                        Instantiate(InsideWall, pos, Quaternion.Euler(0, 0, 90));
                    }
                }

                else if (spriteValue == 5)
                {
                    Instantiate(NormalPellet, pos, Quaternion.identity);
                }

                else if (spriteValue == 6)
                {
                    Instantiate(PowerPellet, pos, Quaternion.identity);
                }

                else if (spriteValue == 7)
                {
                    if (spriteDown == 0 || spriteDown == 5 || spriteDown == 6)
                    {
                        Instantiate(TWall, pos, Quaternion.Euler(0, 0, 90));
                    }
                    else if (spriteRight == 0 || spriteRight == 5 || spriteRight == 6)
                    {
                        Instantiate(TWall, pos, Quaternion.Euler(0, 0, 180));
                    }
                    else if (spriteUp == 0 || spriteUp == 5 || spriteUp == 6)
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
