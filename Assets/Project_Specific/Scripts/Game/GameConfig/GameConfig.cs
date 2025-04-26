using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using WayneSDK;


[CreateAssetMenu(fileName = "GameConfig", menuName = "Project/GameConfig")]
public class GameConfig : SingletonScriptableObject<GameConfig>
{
    public GameplayVariableEditor Gameplay = new GameplayVariableEditor();
}

[Serializable]
public class GameplayVariableEditor
{
    public PrefabVariableEditor Prefabs;
    public FieldVariableEditor Field;
}
