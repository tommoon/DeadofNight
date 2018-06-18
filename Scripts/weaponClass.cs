using UnityEngine;

[System.Serializable]
public class weapon
{
    public string name;

    public int clipSize;
    public int roundsAvailable;
    public int dmg;
    public int spread;

    public float rateOfFire;
    public float reloadTime;

    [Range(0, 60)]
    public float spreadAngle;
}
