using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class HeartPartTransparencyController : MonoBehaviour
{
    [Header("Heart Model")]
    public Transform heartRoot;

    [Header("UI")]
    public TMP_Dropdown partDropdown;
    public Slider transparencySlider;
    public TMP_Text transparencyLabel;

    private List<Renderer> partRenderers = new List<Renderer>();
    private List<string> partNames = new List<string>();

    void Start()
    {
        if (heartRoot == null)
        {
            Debug.LogError("HeartRoot is not assigned.");
            return;
        }

        SetupPartList();
        SetupUI();

        // Keep all parts opaque at start to avoid transparent sorting problems
        ResetAllPartsOpaque();
    }

    void SetupPartList()
    {
        partRenderers.Clear();
        partNames.Clear();

        // Option 0
        partNames.Add("All Parts");
        partRenderers.Add(null);

        Renderer[] renderers = heartRoot.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            partRenderers.Add(renderer);
            partNames.Add(renderer.gameObject.name);
        }
    }

    void SetupUI()
    {
        if (partDropdown != null)
        {
            partDropdown.ClearOptions();
            partDropdown.AddOptions(partNames);
            partDropdown.onValueChanged.AddListener(OnPartChanged);
        }

        if (transparencySlider != null)
        {
            transparencySlider.minValue = 0.1f;
            transparencySlider.maxValue = 1.0f;
            transparencySlider.value = 1.0f;
            transparencySlider.onValueChanged.AddListener(SetTransparency);
        }
    }

    void OnPartChanged(int index)
    {
        // When choosing a new part, reset all parts to opaque
        ResetAllPartsOpaque();

        if (transparencySlider != null)
        {
            transparencySlider.value = 1.0f;
        }
    }

    public void SetTransparency(float alpha)
    {
        if (partDropdown == null) return;

        int selectedIndex = partDropdown.value;

        // All Parts
        if (selectedIndex == 0)
        {
            for (int i = 1; i < partRenderers.Count; i++)
            {
                SetRendererAlpha(partRenderers[i], alpha);
            }
        }
        else
        {
            // Only selected part becomes transparent.
            // Other parts remain opaque to reduce wrong overlap/render order.
            for (int i = 1; i < partRenderers.Count; i++)
            {
                if (i == selectedIndex)
                    SetRendererAlpha(partRenderers[i], alpha);
                else
                    SetRendererAlpha(partRenderers[i], 1.0f);
            }
        }
        if (transparencyLabel != null)
{
    transparencyLabel.text = "Transparency: " + Mathf.RoundToInt(alpha * 100f) + "%";
}
    }

    void ResetAllPartsOpaque()
    {
        for (int i = 1; i < partRenderers.Count; i++)
        {
            SetRendererAlpha(partRenderers[i], 1.0f);
        }
    }

    void SetRendererAlpha(Renderer renderer, float alpha)
    {
        if (renderer == null) return;

        Material mat = renderer.material;

        Color color;

        if (mat.HasProperty("_BaseColor"))
            color = mat.GetColor("_BaseColor");
        else
            color = mat.color;

        color.a = alpha;

        if (mat.HasProperty("_BaseColor"))
            mat.SetColor("_BaseColor", color);
        else
            mat.color = color;

        SetMaterialSurfaceMode(mat, alpha);
    }

    void SetMaterialSurfaceMode(Material mat, float alpha)
    {
        if (mat == null) return;

        if (alpha >= 0.99f)
        {
            // Opaque mode
            if (mat.HasProperty("_Surface"))
                mat.SetFloat("_Surface", 0);

            mat.SetFloat("_SrcBlend", (float)BlendMode.One);
            mat.SetFloat("_DstBlend", (float)BlendMode.Zero);
            mat.SetFloat("_ZWrite", 1);

            mat.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
            mat.DisableKeyword("_ALPHABLEND_ON");

            mat.renderQueue = -1;
        }
        else
        {
            // Transparent mode
            if (mat.HasProperty("_Surface"))
                mat.SetFloat("_Surface", 1);

            mat.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
            mat.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
            mat.SetFloat("_ZWrite", 0);

            mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            mat.EnableKeyword("_ALPHABLEND_ON");

            mat.renderQueue = (int)RenderQueue.Transparent;
        }
    }
}