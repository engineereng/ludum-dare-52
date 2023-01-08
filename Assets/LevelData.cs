using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData {
    public int highestLevel;
    public LevelData (LevelsUnlocked levels)
    {
        highestLevel = levels.highestLevel;
    }
}
