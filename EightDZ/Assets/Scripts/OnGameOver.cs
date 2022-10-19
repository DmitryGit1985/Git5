using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnGameOver : MonoBehaviour
{
    [SerializeField] Transform gameOver;
    [SerializeField] Button buttonTryAgain;
    [SerializeField] private GameObject characterController;
    [SerializeField] UIHealth uIHealth;
    private CharacterController2D characterController2D;
    private HealthSystem healthSystem;
    private Animator characterAnimator;
    void Start()
    {
        buttonTryAgain.onClick.AddListener(TryAgain);
        if (characterController.TryGetComponent(out CharacterController2D characterController2D)) //[SecuritySafeCritical] ???
        {
            this.characterController2D = characterController2D;
            healthSystem = characterController2D.HealthSystem;
            healthSystem.OnDead += HealthSystem_OnDead;
            characterAnimator = characterController2D.CharacterAnimator;
        }
    }
    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        gameOver.gameObject.SetActive(true);
    }
    private void TryAgain()
    {
        //добавить в HealthSystem event onRespawn;
        gameOver.gameObject.SetActive(false);
        characterController.transform.position = new Vector2(0, -2.93f);
        characterAnimator.SetTrigger("NewSpawn");
        healthSystem.SetHealth(5);
        for (int i = 0; i < (int)healthSystem.GetHealth(); i++)
        {
            uIHealth.transform.GetChild(i).gameObject.SetActive(true);
        }
        characterController2D.IsDeath = false;
    }
}
