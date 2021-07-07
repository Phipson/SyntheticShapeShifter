//Portions Copyright(c) 2021 - InterDigital Communications, Inc.
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;
using DG.Tweening;


public class DynamicAdaptation : MonoBehaviour
{
    [Header("Adaptation Input")]
    [Range(-5.0f, 5.0f)]
    public float valence;
    [Range(-5.0f, 5.0f)]
    public float arousal;

    [Header("Valence/arousal sliders")]
    public Slider valence_slider;
    public Slider arousal_slider;
    public Text valence_text;
    public Text arousal_text;

    [Header("Fade to black UI image")]
    public Image fade_to_black;

    [Header("Audio Settings")]
    public float neutral_audio_threshold = 1.5f;

    AudioSource audioMusic1;
    AudioSource audioMusic2;
    AudioSource randomSFXAudio;
    int activeAudioSource = 0;
    int currentAudio = 0;
    public AudioClip[] audioHighValenceHighArousal;
    public AudioClip[] audioHighValenceLowArousal;
    public AudioClip[] audioLowValenceHighArousal;
    public AudioClip[] audioLowValenceLowArousal;
    public AudioClip[] audioNeutral;

    [Header("Post-processing Settings")]
    public bool override_profiles = true;
    public PostProcessProfile highValenceHighArousalPPProfile;
    public PostProcessProfile highValenceLowArousalPPProfile;
    public PostProcessProfile lowValenceHighArousalPPProfile;
    public PostProcessProfile lowValenceLowArousalPPProfile;
    PostProcessVolume highValenceHighArousalPPVolume;
    PostProcessVolume highValenceLowArousalPPVolume;
    PostProcessVolume lowValenceHighArousalPPVolume;
    PostProcessVolume lowValenceLowArousalPPVolume;

    [Header("Light Settings")]
    public Light adaptiveDirectionalLight;
    public float neutralDirectionalIntensity = 1.28f;
    public float neutralDirectionalShadows = 1.0f;
    public Vector3 neutralDirectionalRotation = new Vector3(60.0f, 0.0f, 0.0f);
    public Color neutralEnvironmentLightSky = new Color(0.9411765f, 0.7529412f, 0.7529412f, 1f);
    public Color neutralEnvironmentLightEquator = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color neutralEnvironmentLightGround = new Color(0.0f, 1.0f, 0.0f, 1.0f);

    public float HVHADirectionalIntensity = 1.28f;
    public float HVHADirectionalShadows = 1.0f;
    public Vector3 HVHADirectionalRotation = new Vector3(60.0f, 0.0f, 0.0f);
    public Color HVHAEnvironmentLightSky = new Color(0.9411765f, 0.7529412f, 0.7529412f, 1f);
    public Color HVHAEnvironmentLightEquator = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color HVHAEnvironmentLightGround = new Color(0.0f, 1.0f, 0.0f, 1.0f);

    public float HVLADirectionalIntensity = 1.28f;
    public float HVLADirectionalShadows = 1.0f;
    public Vector3 HVLADirectionalRotation = new Vector3(60.0f, 0.0f, 0.0f);
    public Color HVLAEnvironmentLightSky = new Color(0.9411765f, 0.7529412f, 0.7529412f, 1f);
    public Color HVLAEnvironmentLightEquator = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color HVLAEnvironmentLightGround = new Color(0.0f, 1.0f, 0.0f, 1.0f);

    public float LVHADirectionalIntensity = 1.28f;
    public float LVHADirectionalShadows = 1.0f;
    public Vector3 LVHADirectionalRotation = new Vector3(60.0f, 0.0f, 0.0f);
    public Color LVHAEnvironmentLightSky = new Color(0.9411765f, 0.7529412f, 0.7529412f, 1f);
    public Color LVHAEnvironmentLightEquator = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color LVHAEnvironmentLightGround = new Color(0.0f, 1.0f, 0.0f, 1.0f);

    public float LVLADirectionalIntensity = 1.28f;
    public float LVLADirectionalShadows = 1.0f;
    public Vector3 LVLADirectionalRotation = new Vector3(60.0f, 0.0f, 0.0f);
    public Color LVLAEnvironmentLightSky = new Color(0.9411765f, 0.7529412f, 0.7529412f, 1f);
    public Color LVLAEnvironmentLightEquator = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color LVLAEnvironmentLightGround = new Color(0.0f, 1.0f, 0.0f, 1.0f);

    [Header("Skybox Settings")]
    public Color neutralSkyColor = new Color(0.45f, 0.6f, 0.85f, 1.0f);
    public Color HVHASkyColor = new Color(0.45f, 0.6f, 0.85f, 1.0f);
    public Color HVLASkyColor = new Color(0.45f, 0.6f, 0.85f, 1.0f);
    public Color LVHASkyColor = new Color(0.45f, 0.6f, 0.85f, 1.0f);
    public Color LVLASkyColor = new Color(0.45f, 0.6f, 0.85f, 1.0f);

    [Header("Adaptive Camera")]
    public CinemachineBrain cameraBrain;
    public float camera1_threshold = 1.0f;
    public float camera2_threshold = 3.5f;
    public CinemachineVirtualCamera neutralCam1;
    public CinemachineVirtualCamera HVHACam1;
    public CinemachineVirtualCamera HVHACam2;
    public CinemachineVirtualCamera HVLACam1;
    public CinemachineVirtualCamera HVLACam2;
    public CinemachineVirtualCamera LVHACam1;
    public CinemachineVirtualCamera LVHACam2;
    public CinemachineVirtualCamera LVLACam1;
    public CinemachineVirtualCamera LVLACam2;

    [Header("Injected objects")]
    public GameObject hvha_object;
    public GameObject lvha_object1;
    public GameObject lvha_object2;
    public GameObject lvha_object3;

    [Header("SFX")]
    public AudioClip[] HVHA_random;
    public AudioClip[] HVLA_random;
    public AudioClip[] LVHA_random;
    public AudioClip[] LVLA_random;
    
    [Range(0.0f, 100.0f)]
    public float random_SFX_probality;
    
    [Range(0.0f, 5.0f)]
    public float random_SFX_threshold;

    CinemachineVirtualCamera current_cam;

    float prev_valence = 0.0f;
    float prev_arousal = 0.0f;

    private void Awake()
    {
        audioMusic1 = gameObject.AddComponent<AudioSource>();
        audioMusic2 = gameObject.AddComponent<AudioSource>();
        randomSFXAudio = gameObject.AddComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioMusic1.clip = audioNeutral[Random.Range(0, audioNeutral.Length)];
        audioMusic1.Play();
        activeAudioSource = 1;
        currentAudio = 0;

        if (override_profiles)
        {
            highValenceHighArousalPPVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, highValenceHighArousalPPProfile.settings.ToArray());
            highValenceHighArousalPPVolume.weight = 0.0f;
            highValenceLowArousalPPVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, highValenceLowArousalPPProfile.settings.ToArray());
            highValenceLowArousalPPVolume.weight = 0.0f;
            lowValenceHighArousalPPVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, lowValenceHighArousalPPProfile.settings.ToArray());
            lowValenceHighArousalPPVolume.weight = 0.0f;
            lowValenceLowArousalPPVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, lowValenceLowArousalPPProfile.settings.ToArray());
            lowValenceLowArousalPPVolume.weight = 0.0f;
        }

        //adaptiveDirectionalLight.intensity = neutralDirectionalIntensity;
        RenderSettings.ambientSkyColor = neutralEnvironmentLightSky;
        RenderSettings.ambientEquatorColor = neutralEnvironmentLightEquator;
        RenderSettings.ambientGroundColor = neutralEnvironmentLightGround;

        if (RenderSettings.skybox.HasProperty("_SkyTint"))
            RenderSettings.skybox.SetColor("_SkyTint", neutralSkyColor);
        DynamicGI.UpdateEnvironment();

        current_cam = neutralCam1;

        hvha_object.SetActive(false);
        lvha_object1.SetActive(false);
        lvha_object2.SetActive(false);
        lvha_object3.SetActive(false);

        valence_slider.onValueChanged.AddListener(delegate { ValenceValueChanged(); });
        arousal_slider.onValueChanged.AddListener(delegate { ArousalValueChanged(); });
    }

    public void ValenceValueChanged()
    {
        valence_text.text = "Valence: " + valence_slider.value;
    }

    public void ArousalValueChanged()
    {
        arousal_text.text = "Arousal: " + arousal_slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            valence = valence_slider.value;
            arousal = arousal_slider.value;
        }

        // Set new music clip each time previous clip ends
        if (!audioMusic1.isPlaying && !audioMusic2.isPlaying)
        {
            Debug.Log("Loop over...");
            if (currentAudio == 0)
            {
                audioMusic1.clip = audioNeutral[Random.Range(0, audioNeutral.Length)];
            }
            else if (currentAudio == 1)
            {
                audioMusic1.clip = audioHighValenceHighArousal[Random.Range(0, audioHighValenceHighArousal.Length)];
            }
            else if (currentAudio == 2)
            {
                audioMusic1.clip = audioHighValenceLowArousal[Random.Range(0, audioHighValenceLowArousal.Length)];
            }
            else if (currentAudio == 3)
            {
                audioMusic1.clip = audioLowValenceHighArousal[Random.Range(0, audioLowValenceHighArousal.Length)];
            }
            else
            { 
                audioMusic1.clip = audioLowValenceLowArousal[Random.Range(0, audioLowValenceLowArousal.Length)];
            }
            audioMusic1.volume = 1.0f;
            audioMusic1.Play();
            activeAudioSource = 1;
        }

        if(!randomSFXAudio.isPlaying)
        {
            if(Random.Range(0.0f, 100.0f) < random_SFX_probality)
            {
                if(valence > random_SFX_threshold && arousal > random_SFX_threshold)
                    randomSFXAudio.clip = HVHA_random[Random.Range(0, HVHA_random.Length)];
                if(valence > random_SFX_threshold && arousal < -random_SFX_threshold)
                    randomSFXAudio.clip = HVLA_random[Random.Range(0, HVLA_random.Length)];
                if (valence < -random_SFX_threshold && arousal > random_SFX_threshold)
                    randomSFXAudio.clip = LVHA_random[Random.Range(0, LVHA_random.Length)];
                if (valence < -random_SFX_threshold && arousal < -random_SFX_threshold)
                    randomSFXAudio.clip = LVLA_random[Random.Range(0, LVLA_random.Length)];
                randomSFXAudio.Play();
            }
        }

        // Cross fade music if song set needs to be changed
        // Interpolate between post-processing profiles according to the valence/arousal values
        // And set the ligting angle
        if (valence != prev_valence || arousal != prev_arousal) {
            prev_valence = valence;
            prev_arousal = arousal;

            //Music clip change with a cross fade
            int audioNow = currentAudio;
            if (Mathf.Abs(valence) <= neutral_audio_threshold && Mathf.Abs(arousal) <= neutral_audio_threshold)
            {
                if (currentAudio != 0)
                {
                    if (activeAudioSource == 1)
                    {
                        audioMusic2.clip = audioNeutral[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 2;
                    }
                    else
                    {
                        audioMusic1.clip = audioNeutral[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 1;
                    }
                }
                currentAudio = 0;
            }
            else if (valence >= 0.0f && arousal >= 0.0f)
            {
                if (currentAudio != 1)
                {
                    if (activeAudioSource == 1)
                    {
                        audioMusic2.clip = audioHighValenceHighArousal[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 2;
                    }
                    else
                    {
                        audioMusic1.clip = audioHighValenceHighArousal[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 1;
                    }
                }
                currentAudio = 1;
            }
            else if (valence >= 0.0f && arousal < 0.0f)
            {
                if (currentAudio != 2)
                {
                    if (activeAudioSource == 1)
                    {
                        audioMusic2.clip = audioHighValenceLowArousal[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 2;
                    }
                    else
                    {
                        audioMusic1.clip = audioHighValenceLowArousal[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 1;
                    }
                }
                currentAudio = 2;
            }
            else if (valence < 0.0f && arousal >= 0.0f)
            {
                if (currentAudio != 3)
                {
                    if (activeAudioSource == 1)
                    {
                        audioMusic2.clip = audioLowValenceHighArousal[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 2;
                    }
                    else
                    {
                        audioMusic1.clip = audioLowValenceHighArousal[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 1;
                    }
                }
                currentAudio = 3;
            }
            else
            {
                if (currentAudio != 4)
                {
                    if (activeAudioSource == 1)
                    {
                        audioMusic2.clip = audioLowValenceLowArousal[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 2;
                    }
                    else
                    {
                        audioMusic1.clip = audioLowValenceLowArousal[Random.Range(0, audioNeutral.Length)];
                        activeAudioSource = 1;
                    }
                }
                currentAudio = 4;
            }
            //perform cross fade if needed
            if(audioNow != currentAudio)
            {
                Debug.Log("Audio cross fading...");
                if(activeAudioSource == 1)
                {
                    audioMusic1.Play();
                    audioMusic1.volume = 0.0f;
                    DOTween.To(() => audioMusic1.volume, x => audioMusic1.volume = x, 1.0f, 5.0f);
                    DOTween.Sequence()
                        .Append(DOTween.To(() => audioMusic2.volume, x => audioMusic2.volume = x, 0.0f, 5.0f))
                        .OnComplete(() =>
                        {
                            audioMusic2.Stop();
                        });
                }
                else
                {
                    audioMusic2.Play();
                    audioMusic2.volume = 0.0f;
                    DOTween.To(() => audioMusic2.volume, x => audioMusic2.volume = x, 1.0f, 5.0f);
                    DOTween.Sequence()
                        .Append(DOTween.To(() => audioMusic1.volume, x => audioMusic1.volume = x, 0.0f, 5.0f))
                        .OnComplete(() =>
                        {
                            audioMusic1.Stop();
                        });
                }
            }
      
            float lvha = 0.0f;
            float lvla = 0.0f;
            float hvha = 0.0f;
            float hvla = 0.0f;
            //lowValenceLowArousalPPVolume.weight = Mathf.Sin(Time.realtimeSinceStartup);
            if (Mathf.Abs(valence) > Mathf.Abs(arousal))
            {
                if (valence < 0.0f)
                {
                    lvha = ((arousal+Mathf.Abs(valence)) / (2.0f*Mathf.Abs(valence))) * (Mathf.Abs(valence)/5.0f);
                    lvla = ((2.0f * Mathf.Abs(valence) - (arousal + Mathf.Abs(valence))) / (2.0f * Mathf.Abs(valence))) * (Mathf.Abs(valence) / 5.0f);
                }
                else if (valence > 0.0)
                {
                    hvha = ((arousal + Mathf.Abs(valence)) / (2.0f * Mathf.Abs(valence))) * (Mathf.Abs(valence) / 5.0f);
                    hvla = ((2.0f * Mathf.Abs(valence) - (arousal + Mathf.Abs(valence))) / (2.0f * Mathf.Abs(valence))) * (Mathf.Abs(valence) / 5.0f);
                }
            }
            else
            {
                if (arousal < 0.0f)
                {
                    hvla = ((valence + Mathf.Abs(arousal)) / (2.0f * Mathf.Abs(arousal))) * (Mathf.Abs(arousal) / 5.0f);
                    lvla = ((2.0f * Mathf.Abs(arousal) - (valence + Mathf.Abs(arousal))) / (2.0f * Mathf.Abs(arousal))) * (Mathf.Abs(arousal) / 5.0f);
                }
                else if (arousal > 0.0)
                {
                    hvha = ((valence + Mathf.Abs(arousal)) / (2.0f * Mathf.Abs(arousal))) * (Mathf.Abs(arousal) / 5.0f);
                    lvha = ((2.0f * Mathf.Abs(arousal) - (valence + Mathf.Abs(arousal))) / (2.0f * Mathf.Abs(arousal))) * (Mathf.Abs(arousal) / 5.0f);
                }
            }

            if (override_profiles)
            {
                DOTween.To(() => highValenceHighArousalPPVolume.weight, x => highValenceHighArousalPPVolume.weight = x, hvha, 3.0f);
                DOTween.To(() => highValenceLowArousalPPVolume.weight, x => highValenceLowArousalPPVolume.weight = x, hvla, 3.0f);
                DOTween.To(() => lowValenceHighArousalPPVolume.weight, x => lowValenceHighArousalPPVolume.weight = x, lvha, 3.0f);
                DOTween.To(() => lowValenceLowArousalPPVolume.weight, x => lowValenceLowArousalPPVolume.weight = x, lvla, 3.0f);
            }

            DOTween.To(() => highValenceHighArousalPPVolume.weight, x => highValenceHighArousalPPVolume.weight = x, hvha, 3.0f);

            // Adjust ligting angle according to the valence values
            Vector3 new_rot = hvha * HVHADirectionalRotation + hvla * HVLADirectionalRotation + lvha * LVHADirectionalRotation + lvla * LVLADirectionalRotation;
            float new_intensity = hvha * HVHADirectionalIntensity + hvla * HVLADirectionalIntensity + lvha * LVHADirectionalIntensity + lvla * LVLADirectionalIntensity;
            float new_shadow_strength = hvha * HVHADirectionalShadows + hvla * HVLADirectionalShadows + lvha * LVHADirectionalShadows + lvla * LVLADirectionalShadows;
            Color new_ambient_sky = hvha * HVHAEnvironmentLightSky + hvla * HVLAEnvironmentLightSky + lvha * LVHAEnvironmentLightSky + lvla * LVLAEnvironmentLightSky;
            Color new_ambient_equator = hvha * HVHAEnvironmentLightEquator + hvla * HVLAEnvironmentLightEquator + lvha * LVHAEnvironmentLightEquator + lvla * LVLAEnvironmentLightEquator;
            Color new_ambient_ground = hvha * HVHAEnvironmentLightGround + hvla * HVLAEnvironmentLightGround + lvha * LVHAEnvironmentLightGround + lvla * LVLAEnvironmentLightGround;
            Color new_skybox_color = hvha * HVHASkyColor + hvla * HVLASkyColor + lvha * LVHASkyColor + lvla * LVLASkyColor;
            float new_multiplier = 0.0f;
            if (Mathf.Abs(arousal) > Mathf.Abs(valence))
                new_multiplier = Mathf.Abs(arousal) / 5.0f;
            else
                new_multiplier = Mathf.Abs(valence) / 5.0f;
            new_rot = new_multiplier * new_rot + (1.0f-new_multiplier) * neutralDirectionalRotation;
            new_intensity = new_multiplier * new_intensity + (1.0f - new_multiplier) * neutralDirectionalIntensity;
            new_shadow_strength = new_multiplier * new_shadow_strength + (1.0f - new_multiplier) * neutralDirectionalShadows;
            new_ambient_sky = new_multiplier * new_ambient_sky + (1.0f - new_multiplier) * neutralEnvironmentLightSky;
            new_ambient_equator = new_multiplier * new_ambient_equator + (1.0f - new_multiplier) * neutralEnvironmentLightEquator;
            new_ambient_ground = new_multiplier * new_ambient_ground + (1.0f - new_multiplier) * neutralEnvironmentLightGround;
            new_skybox_color = new_multiplier * new_skybox_color + (1.0f - new_multiplier) * neutralSkyColor;
            Debug.Log(new_rot);
            Debug.Log(new_intensity);
            adaptiveDirectionalLight.transform.DORotate(new_rot, 5);
            
            DOTween.To(() => adaptiveDirectionalLight.intensity, x => adaptiveDirectionalLight.intensity = x, new_intensity, 5.0f);
            DOTween.To(() => adaptiveDirectionalLight.shadowStrength, x => adaptiveDirectionalLight.shadowStrength = x, new_shadow_strength, 5.0f);

            RenderSettings.ambientSkyColor = new_ambient_sky;
            RenderSettings.ambientEquatorColor = new_ambient_equator;
            RenderSettings.ambientGroundColor = new_ambient_ground;

            if (RenderSettings.skybox.HasProperty("_SkyTint"))
                RenderSettings.skybox.SetColor("_SkyTint", new_skybox_color);

            DynamicGI.UpdateEnvironment();

            //Camera control
            CinemachineVirtualCamera moveto_cam = null;
            float cam1threshold = camera1_threshold;
            float cam2threshold = camera2_threshold;

            if (Mathf.Abs(valence) < cam1threshold && Mathf.Abs(arousal) < cam1threshold)
            {
                Debug.Log("neutralCam1");
                moveto_cam = neutralCam1;
                if(current_cam == HVHACam1 || current_cam == HVHACam2)
                {
                    cameraBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
                    cameraBrain.m_DefaultBlend.m_Time = 20.0f;
                }
                else
                {   
                    cameraBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
                }
            }
            else if(valence >= 0.0f && arousal >= 0.0f)
            {
                Debug.Log("HVHACam1");
                moveto_cam = HVHACam1;
                if (valence > cam2threshold && arousal > cam2threshold)
                    moveto_cam = HVHACam2;

                if(current_cam == neutralCam1 || current_cam == HVHACam1 || current_cam == HVHACam2)
                {
                    cameraBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
                    cameraBrain.m_DefaultBlend.m_Time = 20.0f;
                }
                else
                {
                    cameraBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
                }
            }
            else if(valence >= 0.0f && arousal < 0.0f)
            {
                Debug.Log("HVLACam1");
                moveto_cam = HVLACam1;
                if (valence > cam2threshold && arousal < -cam2threshold)
                    moveto_cam = HVLACam2;

                if (current_cam == HVLACam1 || current_cam == HVLACam2)
                {
                    cameraBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
                    cameraBrain.m_DefaultBlend.m_Time = 20.0f;
                }
                else
                {
                    cameraBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
                }
            }
            else 
            {
                if (valence < 0.0f && arousal > 0.0f)
                {
                    Debug.Log("LVHACam1");
                    moveto_cam = LVHACam1;
                    if (valence < -cam2threshold && arousal > cam2threshold)
                        moveto_cam = LVHACam2;
                }
                else
                {
                    Debug.Log("LVLACam1");
                    moveto_cam = LVLACam1;
                    if (valence < -cam2threshold && arousal <= -cam2threshold)
                        moveto_cam = LVLACam2;
                }
                if (current_cam == LVHACam1 || current_cam == LVHACam2 || current_cam == LVLACam1 || current_cam == LVLACam2)
                {
                    Debug.Log("BLEND");
                    cameraBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
                    cameraBrain.m_DefaultBlend.m_Time = 20.0f;
                }
                else
                {
                    Debug.Log("CUT");
                    cameraBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
                }
            }

            Debug.Log(current_cam);
            Debug.Log(moveto_cam);
            CinemachineVirtualCamera tmp_cam = null;


            if (current_cam != moveto_cam)
            {
                Debug.Log("Change camera!");
                if (cameraBrain.m_DefaultBlend.m_Style == CinemachineBlendDefinition.Style.Cut)
                {
                    tmp_cam = current_cam;
                    current_cam = moveto_cam;

                    DOTween.Sequence()
                    .Append(fade_to_black.DOColor(new Color32(0, 0, 0, 255), 0.2f))
                    .OnComplete(() =>
                    {
                        cameraBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
                        tmp_cam.m_Priority = 10;
                        moveto_cam.m_Priority = 11;
                        fade_to_black.DOColor(new Color32(0, 0, 0, 0), 1.0f);
                    });
                    
                    //fade_to_black.color = new Color32(0, 0, 0, 255);
                }
                else
                {
                    current_cam.m_Priority = 10;
                    moveto_cam.m_Priority = 11;
                    current_cam = moveto_cam;
                }
            }
        }

        if (valence < -4.0f && arousal > 4.0f)
        {
            if (current_cam == LVHACam2)
            {
                if (cameraBrain.ActiveBlend != null)
                {
                    if (cameraBrain.ActiveBlend.TimeInBlend >= 10.0f)
                    {
                        //Debug.Log(cameraBrain.ActiveBlend.TimeInBlend);
                        DOTween.Sequence()
                        .Append(DOTween.To(() => current_cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain, x => current_cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = x, 1.0f, 15.0f))
                        .OnComplete(() =>
                        {
                            lvha_object1.SetActive(true);
                            StartCoroutine(delayedAcitvate(lvha_object2, 3));
                            StartCoroutine(delayedAcitvate(lvha_object3, 5));
                        });
                    }
                }
            }
        }
        else
        {
            LVHACam2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
        }
        if (valence > 4.0f && arousal > 4.0f)
        {
            if (current_cam == HVHACam2)
            {
                if (cameraBrain.ActiveBlend != null)
                    if (cameraBrain.ActiveBlend.TimeInBlend >= 10.0f)
                        hvha_object.SetActive(true);
            }
        }
        else if(current_cam == neutralCam1)
        {
            hvha_object.SetActive(false);
            lvha_object1.SetActive(false);
            lvha_object2.SetActive(false);
            lvha_object3.SetActive(false);
        }
    }
    
    void OnDestroy()
    {
        //RuntimeUtilities.DestroyVolume(neutralPPVolume, true, true);
    }

    IEnumerator delayedAcitvate(GameObject activate, int wait_time)
    {
        yield return new WaitForSeconds(wait_time);
        Debug.Log("delayed activate:" + activate);
        activate.SetActive(true);
    }
}
