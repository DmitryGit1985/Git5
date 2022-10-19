using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float relativeMove;

    void Update()
    {
        transform.position = new Vector2(cam.position.x * relativeMove, cam.position.y);
    }
}
