using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private void Start()
    {
        float newScale = Random.Range(1f, 1.5f);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        float randomYRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);
    }
}
