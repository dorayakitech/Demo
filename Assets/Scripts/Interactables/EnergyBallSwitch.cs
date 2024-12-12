using System.Collections.Generic;
using UnityEngine;

public class EnergyBallSwitch : EnergyBallReceiver, IAppearanceChangeable
{
    public void ChangeAppearance(Material newMaterial)
    {
        currentMaterials = new List<Material> { newMaterial };
    }
}