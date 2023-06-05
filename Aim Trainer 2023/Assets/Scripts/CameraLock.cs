using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public Transform camreaPosition;
    // Update is called once per frame
    void Update()
    {
        transform.position = camreaPosition.position;
    }
}
