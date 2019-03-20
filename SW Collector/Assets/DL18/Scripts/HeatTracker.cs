using UnityEngine;
using UnityEngine.UI;

public class HeatTracker : MonoBehaviour {

    public Image heatLeft;
    public Image heatRight;

    private float maxHeat = 1f;
    private float coolingRate = 0f;

    private float currentHeat = 0f;

    private float cd = 2f;

    private void Update()
    {
        if (currentHeat > 0f)
        {
            cd -= Time.deltaTime;

            if (cd <= 0f)
            {
                if (currentHeat >= maxHeat)
                {
                    SendMessage("OverheatGun", false);
                }
                ModifyHeat(-coolingRate);
                SetCD(Time.deltaTime * coolingRate);
            }
        }
    }

    public void ModifyHeat(float heat)
    {
        currentHeat += heat;

        if (currentHeat >= maxHeat)
        {
            currentHeat = maxHeat;
            SendMessage("OverheatGun", true);
        }

        if (currentHeat < 0f)
        {
            currentHeat = 0f;
        }

        heatLeft.fillAmount = currentHeat / 4f;
        heatRight.fillAmount = currentHeat / 4f;

        SetCD(2f);
    }

    public void SetCooling(float newCoolingRate)
    {
        coolingRate = newCoolingRate;
    }

    public void SetCD(float value)
    {
        cd = value;
    }
}
