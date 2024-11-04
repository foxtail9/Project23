using UnityEngine.UI;
using UnityEngine;

public class PlyerUIManager : MonoBehaviour
{
    [SerializeField] private PlayerConditions playerConditions;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private GameObject damageui;

    void Update()
    {
        UpdateHealthBar();
        UpdateStaminaBar();
        if(playerConditions.curValueHP < 50)
        {
            damageui.SetActive(true);
        }
        else
        {
            damageui.SetActive(false);
        }
    }

    private void UpdateHealthBar()
    {
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
}
