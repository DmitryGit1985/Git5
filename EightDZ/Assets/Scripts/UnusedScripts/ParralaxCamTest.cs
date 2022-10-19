using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxCamTest : MonoBehaviour
{
    [SerializeField] private float parallax;
    private float startpos;
    [SerializeField] private Transform ñameraTransform;
    private Vector3 targetPos;
    private float distance;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;
    private float textureUnitSizeX;

    void Start()
    {
        startpos = transform.position.x;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    void Update()
    {
        distance = ñameraTransform.position.x * parallax;
        targetPos = new Vector3(startpos + distance, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);//Êàê ñëî
        float offsetPositionX = (ñameraTransform.position.x - transform.position.x) % textureUnitSizeX;
        //Debug.Log(ñameraTransform.position.x+" "+ transform.position.x);
        Debug.Log(offsetPositionX);
        //Debug.Log(targetPos);

        /*if (Mathf.Abs(ñameraTransform.position.x - targetPos.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (ñameraTransform.position.x - targetPos.x) % textureUnitSizeX;
            transform.position = new Vector3(ñameraTransform.position.x + offsetPositionX, transform.position.y, transform.position.z);
        }*/
    }
}
