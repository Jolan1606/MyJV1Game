using UnityEngine;

public class Watersource : MonoBehaviour

{
    public GameObject waterSource; // Référence à l’objet de la source d’eau
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        if (waterSource == null)
        {
            waterSource = Object.FindAnyObjectByType<Watersource>().gameObject;
            if (waterSource == null)
                Debug.LogError("WaterSource introuvable ! Vérifie que ton WaterSource est bien dans la scène.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            WaterSlider waterSlider = collision.gameObject.GetComponent<WaterSlider>();
            if (waterSlider != null)
            {

                waterSlider.water = waterSlider.MaxWater;
                waterSlider.slider.value = waterSlider.MaxWater;
                waterSource.SetActive(false);

            }
        }
    }


}
