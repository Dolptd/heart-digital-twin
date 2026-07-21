using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhysiologicalHeartbeatController : MonoBehaviour{
    [Header("Heart Parts")]
    public Transform leftAtrium;
    public Transform rightAtrium;
    public Transform leftVentricle;
    public Transform rightVentricle;
    public Transform myocardium;

    [Header("Major Vessels")]
    public Transform aorta;
    public Transform pulmonaryArtery;

    [Header("BPM Control")]
    public Slider bpmSlider;
    public TMP_Text bpmLabel;
    public float bpm = 70f;

    [Header("Motion Strength")]
    public float atriumAmplitude = 0.035f;
    public float ventricleAmplitude = 0.065f;
    public float myocardiumAmplitude = 0.045f;

    [Header("Timing Offset")]
    [Range(0f, 1f)] public float atriumPhase = 0.05f;
    [Range(0f, 1f)] public float ventriclePhase = 0.32f;

    private Vector3 leftAtriumScale;
    private Vector3 rightAtriumScale;
    private Vector3 leftVentricleScale;
    private Vector3 rightVentricleScale;
    private Vector3 myocardiumScale;
    private Vector3 aortaScale;
    private Vector3 pulmonaryArteryScale;

     void Start()
    {
        CacheOriginalScales();

        if (bpmSlider != null)
        {
            bpmSlider.minValue = 40f;
            bpmSlider.maxValue = 160f;
            bpmSlider.value = bpm;
            bpmSlider.wholeNumbers = true;
            bpmSlider.onValueChanged.AddListener(SetBPM);
        }
        SetBPM(bpm);
    }

    void Update()
    {
        float cycleDuration = 60f / bpm;
        float phase = (Time.time % cycleDuration) / cycleDuration;

        float atriumPulse = HeartPulse(phase, atriumPhase, 0.18f);
        float ventriclePulse = HeartPulse(phase, ventriclePhase, 0.24f);

        // Atria contract first: slightly shrink during atrial systole
        ApplyScale(leftAtrium, leftAtriumScale, 1f - atriumPulse * atriumAmplitude);
        ApplyScale(rightAtrium, rightAtriumScale, 1f - atriumPulse * atriumAmplitude);

        // Ventricles contract after atria: stronger motion
        ApplyScale(leftVentricle, leftVentricleScale, 1f - ventriclePulse * ventricleAmplitude);
        ApplyScale(rightVentricle, rightVentricleScale, 1f - ventriclePulse * ventricleAmplitude);

        // Myocardium follows ventricular contraction
        ApplyScale(myocardium, myocardiumScale, 1f - ventriclePulse * myocardiumAmplitude);

        // Keep arteries static
ApplyScale(aorta, aortaScale, 1f);
ApplyScale(pulmonaryArtery, pulmonaryArteryScale, 1f);

    }

    float HeartPulse(float phase, float peakPhase, float width)
    {
        float distance = Mathf.Abs(Mathf.DeltaAngle(phase * 360f, peakPhase * 360f)) / 360f;
        float pulse = Mathf.Exp(-(distance * distance) / (2f * width * width));
        return pulse;
    }

    void ApplyScale(Transform part, Vector3 originalScale, float scaleFactor)
    {
        if (part == null) return;
        part.localScale = originalScale * scaleFactor;
    }

    void CacheOriginalScales()
    {
        if (leftAtrium != null) leftAtriumScale = leftAtrium.localScale;
        if (rightAtrium != null) rightAtriumScale = rightAtrium.localScale;
        if (leftVentricle != null) leftVentricleScale = leftVentricle.localScale;
        if (rightVentricle != null) rightVentricleScale = rightVentricle.localScale;
        if (myocardium != null) myocardiumScale = myocardium.localScale;

        if (aorta != null) aortaScale = aorta.localScale;
        if (pulmonaryArtery != null) pulmonaryArteryScale = pulmonaryArtery.localScale;
    }

    public void SetBPM(float value)
    {
        bpm = value;
        if (bpmLabel != null)
    {
        bpmLabel.text = "BPM: " + Mathf.RoundToInt(bpm);
    }
    }
}
