using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaterSlider : MonoBehaviour
{
   public float water;
   public float MaxWater;
   public Slider slider;
   public HealthSlider healthSlider;
   
    void Start()
    {
        if (healthSlider == null)
        {
            healthSlider = Object.FindAnyObjectByType<HealthSlider>();

            if (healthSlider == null)
                Debug.LogError("HealthSlider introuvable ! Vérifie que ton Player est bien dans la scène.");
        }
        MaxWater = 100f;
        water = MaxWater;
        slider.maxValue = MaxWater;
        slider.value = water;
        StartCoroutine(DecreaseWater());
    }

    // Update is called once per frame
    void Update()
    {
        if (water <= 0)
        {
            Debug.Log("Out of water!");
           
            water = MaxWater;
            slider.value = MaxWater;
           healthSlider.health = healthSlider.health - 20;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            water = water - 10;
            slider.value = water;
        }
    }

    IEnumerator DecreaseWater()
    {
        while (water > 0)
        {
            water -= 1;
            slider.value = water;
            yield return new WaitForSeconds(1f);
        }
    }
    


}

