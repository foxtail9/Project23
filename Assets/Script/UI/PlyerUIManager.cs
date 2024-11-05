using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlyerUIManager : MonoBehaviour
{
    [SerializeField] private PlayerConditions playerConditions;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private GameObject damageui;

    public List<TextMeshProUGUI> texts;
    public float fadeDuration = 5f;

    private void Start()
    {
        StartCoroutine(FadeOutAllTexts());
    }

    void Update()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
    }

    private void UpdateHealthBar()
    {
        damageui.SetActive(playerConditions.curValueHP < 50);

        if (healthBar != null)
        {
            float healthFillAmount = 1 - (playerConditions.curValueHP / playerConditions.maxValueHP);
            healthBar.fillAmount = healthFillAmount;
        }
    }

    private void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            float staminaFillAmount = playerConditions.curValueStamina / playerConditions.maxValueStamina;
            staminaBar.fillAmount = staminaFillAmount;
        }
    }

    private IEnumerator FadeOutAllTexts()
    {
        foreach (var text in texts)
        {
            Color originalColor = text.color;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        }

        float currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, currentTime / fadeDuration);

            foreach (var text in texts)
            {
                Color originalColor = text.color;
                text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            }
            
            yield return null;
        }

        foreach (var text in texts)
        {
            Color originalColor = text.color;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            foreach (var textss in texts)
            {
                textss.gameObject.SetActive(false);
            }
        }
    }
}
