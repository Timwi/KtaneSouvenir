using System.Collections.Generic;
using UnityEngine;

public class TestHighlightable : MonoBehaviour
{
    public Vector3 HighlightScale = Vector3.zero;
    public GameObject HighlightPrefab;

    [Range(0.0F, 0.01f)]
    public float OutlineAmount = 0f;

    public Material MaterialOverride = null;

    protected static readonly string interactionAnimationState = "InteractionPulse";
    protected static readonly string selectionAnimationState = "SelectionPulse";
    protected GameObject highlight;
    protected Animator highlightAnimator;

    protected virtual void Start()
    {
        Init();
    }

    protected void Init()
    {
        if (highlight == null && GetComponent<MeshFilter>() != null)
        {
            highlight = Instantiate(HighlightPrefab);
            highlight.transform.parent = transform;

            if (HighlightScale == Vector3.zero)
            {
                if (OutlineAmount == 0f)
                {
                    highlight.transform.localScale = new Vector3(1.1f, 0.56f, 1.1f);
                }
                else
                {
                    highlight.transform.localScale = Vector3.one;
                }
            }
            else
            {
                highlight.transform.localScale = HighlightScale;
            }

            highlight.transform.localPosition = new Vector3(0, 0, 0);
            highlight.transform.localRotation = Quaternion.identity;

            MeshFilter meshFilter = highlight.AddComponent<MeshFilter>();
            if (GetComponent<MeshFilter>() == null || GetComponent<MeshFilter>().sharedMesh == null)
            {
                Debug.LogErrorFormat(@"The following object’s highlightable does not have a mesh. Make sure it has a MeshFilter component with an assigned mesh: {0}", TestHarness.GetObjectPath(transform));
                return;
            }
            meshFilter.mesh = Instantiate(GetComponent<MeshFilter>().sharedMesh);

            int materialCount = 1;
            if (GetComponent<Renderer>() != null)
            {
                materialCount = Mathf.Max(GetComponent<Renderer>().sharedMaterials.Length, materialCount);
            }

            Material highlightMaterial = MaterialOverride;
            if (MaterialOverride == null)
            {
                highlightMaterial = highlight.GetComponent<Renderer>().sharedMaterial; // use material in the highlight prefab
            }
            Material[] highlightMaterials = new Material[materialCount];
            for (int i = 0; i < materialCount; i++)
            {
                highlightMaterials[i] = highlightMaterial;
            }
            highlight.GetComponent<Renderer>().materials = highlightMaterials;

            highlight.SetActive(false);
        }
    }

    public void Off()
    {
        On(false);
    }

    public void On()
    {
        On(true);
    }

    protected void On(bool on)
    {
        if (highlight != null)
        {
            highlight.SetActive(on);
        }
    }
}
