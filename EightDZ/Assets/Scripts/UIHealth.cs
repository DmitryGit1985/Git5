using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private GameObject characterController;
    [SerializeField] private Image healthImage;
    private Image instantiatedImage;
    private Image curentHealthImage;
    private HealthSystem healthSystem;
    private SpriteRenderer playerSprite;
    [field: NonReorderable] public List<Image> ImageList { get; private set; }

    private void Start()
    {
        playerSprite = characterController.GetComponent<SpriteRenderer>();
        if (characterController.TryGetComponent(out CharacterController2D characterController2D)) //[SecuritySafeCritical] ???
        {
            healthSystem = characterController2D.HealthSystem;
            healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
            healthSystem.OnDamaged += HealthSystem_OnHealthDamaged;
            healthSystem.OnDead += HealthSystem_OnDead;
        }
        /*if (HealthSystem.TryGetHealthSystem(healthSystemGameObject, out HealthSystem healthSystem))
        {
            SetHealthSystem(healthSystem);
        }*/

        Vector3 firstoffset = new Vector3(5, 5, 0);
        Vector3 offset = new Vector3(50, 0, 0);
        ImageList=new List<Image>();
        for (int i = 0; i < (int)healthSystem.GetHealth(); i++)
        {
            instantiatedImage = Instantiate(healthImage, firstoffset + offset * i, Quaternion.identity, transform);
            ImageList.Add(instantiatedImage);
        }
        curentHealthImage = ImageList[ImageList.Count - 1];
    }
    private void SetHealthSystem(HealthSystem healthSystem)
    {
        if (this.healthSystem != null)
        {
            this.healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
        }
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        if (ImageList.Count > 0)
        {
            StartCoroutine(BlinkPlayer());
            //Destroy(imageList[imageList.Count - 1].gameObject); как в рантайме отлавливать если уничтожаютс€ gameobject?
            ImageList[ImageList.Count - 1].gameObject.SetActive(false);
            ImageList.RemoveAt(ImageList.Count - 1);
            if (ImageList.Count > 1)
            {
                curentHealthImage = ImageList[ImageList.Count - 1];
            }
        }
    }
    private void HealthSystem_OnHealthDamaged(object sender, System.EventArgs e)
    {
        if (ImageList.Count > 1)
        {
            StartCoroutine(BlinkHealthImage());
        }
    }
    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        playerSprite.color = new Color(1, 1, 1, 1);
    }
        private void OnDestroy()
    {
        //healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
    }
    private IEnumerator BlinkPlayer()
    {
        Color defaultColor = playerSprite.color;
        for (int i = 0; i < 5; i++)
        {
                playerSprite.color = new Color(0, 0, 0, 0.5f);
                yield return new WaitForSeconds(0.1f);
                playerSprite.color = defaultColor;
                yield return new WaitForSeconds(0.1f);
        }
                playerSprite.color = defaultColor;
    }
    private IEnumerator BlinkHealthImage()
    {
        Color defaultColor = curentHealthImage.color;
        for (int i = 0; i < 5; i++)
        {
                curentHealthImage.color = new Color(0, 0, 0, 0.5f);
                yield return new WaitForSeconds(0.1f);
                curentHealthImage.color = defaultColor;
                yield return new WaitForSeconds(0.1f);
        }
            curentHealthImage.color = defaultColor;
    }

}
