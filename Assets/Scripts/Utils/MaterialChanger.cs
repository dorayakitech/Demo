using System.Collections.Generic;
using UnityEngine;

public static class MaterialChanger
{
    public static void ChangeMaterial(Material mat, ref List<MeshRenderer> meshRenderers)
    {
        var newMats = new[] { mat };
        foreach (var mr in meshRenderers)
        {
            mr.materials = newMats;
        }
    }

    public static void FindMeshRenderersRecursively(Transform parent, ref List<MeshRenderer> meshRenderers,
        ref List<Material> defaultMaterials)
    {
        if (parent.TryGetComponent<MeshRenderer>(out var mr))
        {
            meshRenderers.Add(mr);
            defaultMaterials.Add(mr.materials[0]);
        }

        foreach (Transform child in parent)
        {
            FindMeshRenderersRecursively(child, ref meshRenderers, ref defaultMaterials);
        }
    }
}