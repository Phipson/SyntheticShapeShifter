using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SeasonChange: MonoBehaviour
{
    // declaring the 3 versions of terrain in the scene (you will need to assign the gameobjects into these 3 generated fields)
    public GameObject TerrainSummer;
    public GameObject TerrainAutumn;
    public GameObject TerrainWinter;
    // declaring the array of gameobjects, which have regular & snowy versions (you will need to assign them into the [0] & [1] generated fields)
    public GameObject[] MossyRocks;
    public GameObject[] FallenTrunks;
    public GameObject[] FallenBranches;
    public GameObject[] Mushrooms;
    public Color S1AmbSky = new Color(0.2313726f, 0.3019608f, 0.3764706f);
    public Color S1AmbEq = new Color(0.1086686f, 0.1373753f, 0.2075472f);
    public Color S1AmbGr = new Color(0.1411765f, 0.1215686f, 0.09803922f);
    public Color Fog1color = new Color(0.5019608f, 0.6235294f, 0.6196079f);
    public float Fog1Dens;
    public Color S2AmbSky = new Color(0.5215687f, 0.3803922f, 0.2039216f);
    public Color S2AmbEq = new Color(0.1568628f, 0.09803922f, 0.172549f);
    public Color S2AmbGr = new Color(0.3882353f, 0.2509804f, 0.2156863f);
    public Color Fog2color = new Color(0.8901961f, 0.8352941f, 0.7098039f);
    public float Fog2Dens;
    public Color S3AmbSky = new Color(0.7411765f, 0.7450981f, 0.7450981f);
    public Color S3AmbEq = new Color(0.172549f, 0.1921569f, 0.2352941f);
    public Color S3AmbGr = new Color(0.2156863f, 0.3137255f, 0.3882353f);
    public Color Fog3color = new Color(0.8f, 0.8705882f, 0.8862745f);
    public float Fog3Dens;
    // declaring the bool condition (false by default) if you add PostProcessingBehaviour to this gameobject (which you need to set to true/checked if using it), and the array (3) of PostProcessingProfiles to match each of the 3 terrains
    public bool usingPostProcessing = false;
    public PostProcessVolume PostfxScript;
    public PostProcessProfile[] PostfxProfiles;

    void Start()
    {
        // only keep the "TerrainSummer" active at start of play
        Setting1();
    }

    // Update is called once per frame
    void Update()
    {
        // if "1" key pressed 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Setting1();
        }
        // if "2" key pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Setting2();
        }
        // if "3" key pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Setting3();
        }
    }

    void Setting1()
    {
        // activate "TerrainSummer" and deactive the other terrains
        TerrainSummer.SetActive(true);
        TerrainAutumn.SetActive(false);
        TerrainWinter.SetActive(false);
        MossyRocks[0].SetActive(true);
        MossyRocks[1].SetActive(false);
        FallenTrunks[0].SetActive(true);
        FallenTrunks[1].SetActive(false);
        FallenBranches[0].SetActive(true);
        FallenBranches[1].SetActive(false);
        Mushrooms[0].SetActive(true);
        Mushrooms[1].SetActive(false);
        // set the ambient lights
        RenderSettings.ambientSkyColor = S1AmbSky;
        RenderSettings.ambientEquatorColor = S1AmbEq;
        RenderSettings.ambientGroundColor = S1AmbGr;
        // set the fog density down;
        RenderSettings.fogDensity = Fog1Dens;
        RenderSettings.fogColor = Fog1color;
        #region usingPostProcessing?
        if (usingPostProcessing)
        {
            // setting the 1st profile from the array to this gameobject's PostfxScript
            PostfxScript.profile = PostfxProfiles[0];
        }
        #endregion
    }
    void Setting2()
    {
        // activate "TerrainAutumn" and deactive the other terrains
        TerrainAutumn.SetActive(true);
        TerrainSummer.SetActive(false);
        TerrainWinter.SetActive(false);
        MossyRocks[0].SetActive(true);
        MossyRocks[1].SetActive(false);
        FallenTrunks[0].SetActive(true);
        FallenTrunks[1].SetActive(false);
        FallenBranches[0].SetActive(true);
        FallenBranches[1].SetActive(false);
        Mushrooms[0].SetActive(true);
        Mushrooms[1].SetActive(false);
        // set the ambient lights
        RenderSettings.ambientSkyColor = S2AmbSky;
        RenderSettings.ambientEquatorColor = S2AmbEq;
        RenderSettings.ambientGroundColor = S2AmbGr;
        // set the fog density & color
        RenderSettings.fogDensity = Fog2Dens;
        RenderSettings.fogColor = Fog2color;
        #region usingPostProcessing?
        if (usingPostProcessing)
        {
            // setting the 2nd profile from the array to this gameobject's PostfxScript
            PostfxScript.profile = PostfxProfiles[1];
        }
        #endregion
    }
    void Setting3()
    {
        // activate "TerrainWinter" and deactive the other terrains
        TerrainWinter.SetActive(true);
        TerrainSummer.SetActive(false);
        TerrainAutumn.SetActive(false);
        MossyRocks[1].SetActive(true);
        MossyRocks[0].SetActive(false);
        FallenTrunks[1].SetActive(true);
        FallenTrunks[0].SetActive(false);
        FallenBranches[1].SetActive(true);
        FallenBranches[0].SetActive(false);
        Mushrooms[1].SetActive(true);
        Mushrooms[0].SetActive(false);
        // set the ambient lights
        RenderSettings.ambientSkyColor = S3AmbSky;
        RenderSettings.ambientEquatorColor = S3AmbEq;
        RenderSettings.ambientGroundColor = S3AmbGr;
        // set the fog density & color
        RenderSettings.fogDensity = Fog3Dens;
        RenderSettings.fogColor = Fog3color;
        #region usingPostProcessing?
        if (usingPostProcessing)
        {
            // setting the 3rd profile from the array to this gameobject's PostfxScript
            PostfxScript.profile = PostfxProfiles[2];
        }
        #endregion
    }
}
