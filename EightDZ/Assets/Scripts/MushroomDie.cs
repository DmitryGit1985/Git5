using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDie : MonoBehaviour
{
    [SerializeField] private Transform mushroom;
    [SerializeField] private TrapDamage mushroomDamage;
    private SpriteRenderer mushroomSprite;
    private PingPongMove mushroomMove;
    private Animator mushroomAnimator;
    private void Start()
    {
        mushroomMove = mushroom.GetComponent<PingPongMove>();
        mushroomAnimator = mushroom.GetComponent<Animator>();
        mushroomSprite = mushroom.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            mushroomDamage.gameObject.SetActive(false);
            mushroomMove.enabled=false;
            mushroomAnimator.enabled = false;
            mushroom.localScale = new Vector3(mushroom.localScale.x, 0.5f, mushroom.localScale.z);
            StartCoroutine(Blinkmushroom());
            StartCoroutine(DieAfterJump());
        }
    }
    private IEnumerator DieAfterJump()
    {
        yield return new WaitForSeconds(2);
        Destroy(mushroom.gameObject);
    }
    private IEnumerator Blinkmushroom()
    {
        Color defaultColor = mushroomSprite.color;
        for (int i = 0; i < 5; i++)
        {
            mushroomSprite.color = new Color(0, 0, 0, 0.5f);
            yield return new WaitForSeconds(0.1f);
            mushroomSprite.color = defaultColor;
            yield return new WaitForSeconds(0.1f);
        }
        mushroomSprite.color = defaultColor;
    }
}
