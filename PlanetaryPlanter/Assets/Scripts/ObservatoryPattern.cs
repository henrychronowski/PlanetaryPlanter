using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservatoryPattern : MonoBehaviour
{
    public int width; //how many chars in each string?

    public List<string> patternRows;
    public GameObject success;

    public bool[,] boolPattern;

    void ParseListTo2DBoolArray()
    {
        //for(int j = patternRows.Count - 1; j >= 0; j--)
        //{
        //    for(int i = 0; i < width; i++) //if anyone has questions about this ask me it makes sense trust me -Dan
        //    {
        //        if(patternRows[j].ToCharArray()[i] == '1')
        //            boolPattern[i, j] = true;
        //        else
        //            boolPattern[i, j] = false;
        //    }
        //}

        for (int i = 0; i < width; i++)
        {
            int k = 0;
            for (int j = patternRows.Count - 1; j >= 0; j--) //if anyone has questions about this ask me it makes sense trust me -Dan
            {
                if (patternRows[j].ToCharArray()[i] == '1')
                    boolPattern[i, k] = true;
                else
                    boolPattern[i, k] = false;

                k++;
            }
        }

    }

    //bool Compare()
    //{
    //    bool[,] tempArray = new bool[width, patternRows.Count];
    //    ObservatoryPlanetSpot[,] planets = Observatory.instance.planetSpotsArray;

    //    for (int j = patternRows.Count - 1; j >= 0; j--)
    //    {
    //        for (int i = 0; i < width; i++) //if anyone has questions about this ask me it makes sense trust me -Dan
    //        {
    //            if (planets[i, j].filled)
    //                tempArray[i, j] = true;
    //            else
    //                tempArray[i, j] = false;
    //        }
    //    }

    //    for (int i = 0; i < width; i++)
    //    {
    //        for (int j = 0; j < patternRows.Count; j++)
    //        {
    //            if (tempArray[i, j] != boolPattern[i, j])
    //            {
    //                //Debug.Log("False");
    //                return false;
    //            }
    //        }
    //    }

    //    Debug.Log("Potassium");
    //    success.SetActive(true);
    //    return true;
    //}


    // Start is called before the first frame update
    void Start()
    {
        boolPattern = new bool[width, patternRows.Count];
        ParseListTo2DBoolArray();
    }

    // Update is called once per frame
    void Update()
    {
        //Compare();
    }
}
