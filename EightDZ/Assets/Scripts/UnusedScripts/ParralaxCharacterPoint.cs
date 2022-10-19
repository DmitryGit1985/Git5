using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxCharacterPoint : MonoBehaviour
{
    [SerializeField] private Vector2 pararalaxEffectMultiplyer;
    private CharacterController2D characterController2D;
    private Transform characterController2dTransform;
    private Vector3 lastCharacterController2dPosition;
    private float textureUnitSizeX;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPos;

    public CharacterController2D CharacterController2D  { get => characterController2D = characterController2D ?? FindObjectOfType<CharacterController2D>(); }

    private void Start()
    {
        characterController2dTransform = CharacterController2D.transform;
        lastCharacterController2dPosition = characterController2dTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }
    private void LateUpdate()
    {
        Vector3 deltaMovement = characterController2dTransform.position - lastCharacterController2dPosition;

        transform.position += new Vector3(deltaMovement.x*pararalaxEffectMultiplyer.x, 0, transform.position.z);

        lastCharacterController2dPosition = characterController2dTransform.position;
        
        if (Mathf.Abs(characterController2dTransform.position.x-transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (characterController2dTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(characterController2dTransform.position.x+ offsetPositionX, transform.position.y, transform.position.z);
        }
    }
}
/*Example distances are as follows:

Static = 1

Farthest = 0.9

Farther = 0.75

Far = 0.5

Behind = 0.25

Ahead = -0.1

Close = -0.25

Closer = -0.5

Closest = -0.75
*/