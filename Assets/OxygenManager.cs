using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    public float OxygenDepletionRate = 3f;
    public float OxygenMax = 500f;
    public float OxygenMin = 0f;
    public float CurrentOxygen = 300f;
    public float OxygenReplenishRate = 0.2f;

    public Slider OxygenSlider;
    public Image fillImage;             
    public Color fullColor = Color.green;
    public Color emptyColor = Color.red;

    public float OxygenTankRestoreAmount = 180f;

    // Start is called before the first frame update
    void Start()
    {
        if (OxygenSlider != null)
            {
                OxygenSlider.maxValue = OxygenMax;
                OxygenSlider.value = CurrentOxygen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DepleteOxygen();
        if (CurrentOxygen > 0f)
        {
            OxygenSlider.value = CurrentOxygen;
            fillImage.color = Color.Lerp(emptyColor, fullColor, CurrentOxygen / OxygenMax);
        }
        //Debug.Log(CurrentOxygen);
    }

    public void DepleteOxygen()
    {
        CurrentOxygen -= OxygenDepletionRate * Time.deltaTime;
        CurrentOxygen = Mathf.Clamp(CurrentOxygen, OxygenMin, OxygenMax);
    }

    public void ReplenishOxygen()
    {
        CurrentOxygen += OxygenReplenishRate * Time.deltaTime;
        CurrentOxygen = Mathf.Clamp(CurrentOxygen, OxygenMin, OxygenMax);
    }

    public void PickUpOxygenTank()
    {
        Debug.Log("picked up oxygen success");
        CurrentOxygen += OxygenTankRestoreAmount;
    }
}
