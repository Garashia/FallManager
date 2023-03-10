using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandDeath : MonoBehaviour
{

    private const int randMax = 100;
    [Header("100分のrandでtrue")]

    [SerializeField, Range(0, randMax)]
    private int rand;

    // true時に処理して
    public bool RandDeathFlag()
    {
        int rnd = Random.Range(0, randMax);
        if (rnd <= rand) return true;

        return false;
    }

}
