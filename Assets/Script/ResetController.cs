using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResetController : MonoBehaviour
{
    [Header("Heart and Camera")]
    public Transform heartRoot;
    public Camera mainCamera;

    [Header("UI")]
    public TMP_Dropdown partDropdown;
    public Slider transparencySlider;
    public Slider bpmSlider;

    [Header("Default Values")]
    public float defaultBPM = 70f;
    public float defaultTransparency = 1.0f;
    public float defaultCameraFOV = 60f;

    private Vector3 defaultHeartPosition;
    private Quaternion defaultHeartRotation;
    private Vector3 defaultHeartScale;

    private Vector3 defaultCameraPosition;
    private Quaternion defaultCameraRotation;

    void Start()
    {
        SaveDefaultState();
    }

    void SaveDefaultState()
    {
        if (heartRoot != null)
        {
            defaultHeartPosition = heartRoot.position;
            defaultHeartRotation = heartRoot.rotation;
            defaultHeartScale = heartRoot.localScale;
        }

        if (mainCamera != null)
        {
            defaultCameraPosition = mainCamera.transform.position;
            defaultCameraRotation = mainCamera.transform.rotation;
            defaultCameraFOV = mainCamera.fieldOfView;
        }
    }

    public void ResetAll()
    {
        // Reset heart transform
        if (heartRoot != null)
        {
            heartRoot.position = defaultHeartPosition;
            heartRoot.rotation = defaultHeartRotation;
            heartRoot.localScale = defaultHeartScale;
        }

        // Reset camera
        if (mainCamera != null)
        {
            mainCamera.transform.position = defaultCameraPosition;
            mainCamera.transform.rotation = defaultCameraRotation;
            mainCamera.fieldOfView = defaultCameraFOV;
        }

        // Reset dropdown to All Parts
        if (partDropdown != null)
        {
            partDropdown.value = 0;
            partDropdown.RefreshShownValue();
        }

        // Reset transparency slider
        if (transparencySlider != null)
        {
            transparencySlider.value = defaultTransparency;
        }

        // Reset BPM slider
        if (bpmSlider != null)
        {
            bpmSlider.value = defaultBPM;
        }
    }
}