using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChangeAppearanceCommand : ICommand
{
    [SerializeField, Required, AssetsOnly] private Material _newMaterial;

    public void Execute<T>(T receiver)
    {
        if (receiver is not MonoBehaviour obj) return;

        Debug.Log("Ready to Change Appearance");

        List<MeshRenderer> meshRenderers = new();
        List<Material> defaultMaterials = new();

        MaterialChanger.FindMeshRenderersRecursively(obj.transform, ref meshRenderers, ref defaultMaterials);

        Debug.Log("Mesh Renderers: " + meshRenderers.Count);

        MaterialChanger.ChangeMaterial(_newMaterial, ref meshRenderers);
    }
}