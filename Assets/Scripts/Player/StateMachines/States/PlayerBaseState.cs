﻿using Animancer.FSM;
using Sirenix.Serialization;
using UnityEngine;

public abstract class PlayerBaseState : StateBehaviour, ISerializationCallbackReceiver, ISupportsPrefabSerialization
{
    [SerializeField, HideInInspector] private SerializationData serializationData;

    SerializationData ISupportsPrefabSerialization.SerializationData
    {
        get => serializationData;
        set => serializationData = value;
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        UnitySerializationUtility.DeserializeUnityObject(this, ref serializationData);
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        UnitySerializationUtility.SerializeUnityObject(this, ref serializationData);
    }
}