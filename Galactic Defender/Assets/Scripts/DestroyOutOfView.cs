using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfView : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}