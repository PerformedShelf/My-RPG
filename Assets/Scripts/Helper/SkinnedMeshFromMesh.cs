using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SkinnedMeshFromMesh : MonoBehaviour {

    private MeshFilter filter;

    void Start() {
        if (GetComponent<MeshRenderer>() && GetComponent<MeshFilter>()) {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            MeshFilter filter = GetComponent<MeshFilter>();
            SkinnedMeshRenderer renderer = gameObject.AddComponent<SkinnedMeshRenderer>();
            renderer.sharedMesh = filter.sharedMesh;
            renderer.sharedMaterials = meshRenderer.sharedMaterials;
            DestroyImmediate(meshRenderer);
            DestroyImmediate(filter);
            DestroyImmediate(this);
        }
    }

}