using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class C_StageManager : MonoBehaviour
{
    public static C_StageManager instance = null;

    [Tooltip("ˆÚ“®‰Â”\”ÍˆÍ")] public BoxCollider2D area;
    [SerializeField] private PostProcessVolume volume;
    [SerializeField] private float vignetteMaxVal;

    private PostProcessProfile profile;
    private Vignette vignette;

    private void Awake()
    {
        instance = this;
        profile = volume.profile;
        vignette = profile.GetSetting<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(0f);
    }

    public void InHide()
    {
        vignette.intensity.Override(vignetteMaxVal);
    }

    public void OutHide()
    {
        vignette.intensity.Override(0f);
    }
}
