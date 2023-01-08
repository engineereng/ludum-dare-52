using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsUnlocked : MonoBehaviour {
    public int highestLevel = 0;

    public void SaveLevelsUnlocked()
    {
        SaveSystem.SaveLevelsUnlocked(this);
    }

    public void LoadLevelsUnlocked()
    {
        LevelData data = SaveSystem.LoadLevelsUnlocked();
        highestLevel = data.highestLevel;
    }
    
}
