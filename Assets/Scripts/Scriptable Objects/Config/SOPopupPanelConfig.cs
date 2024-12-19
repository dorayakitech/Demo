﻿using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SOPopupPanelConfig", menuName = "Scriptable Object/Configs/Popup Panel")]
public class SOPopupPanelConfig : SerializedScriptableObject
{
    [SerializeField, Required] public string Title;
    [SerializeField, Required] public Sprite Image;
    [SerializeField, Required] public float ImageWidth;
    [SerializeField, Required] public float ImageHeight;
    [SerializeField] public string Tutorial;
    [SerializeField] public List<ICommand> TasksAfterClose = new();
}