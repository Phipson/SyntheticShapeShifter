using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace InterDigital.CMU
{
    public class SeasonChange : Singleton<SeasonChange>
    {
        public GameObject TerrainSummer;
        public GameObject TerrainAutumn;
        public GameObject TerrainWinter;
        public GameObject[] MossyRocks;
        public GameObject[] FallenTrunks;
        public GameObject[] FallenBranches;
        public GameObject[] Mushrooms;
        public GameObject[] House;

        void Start()
        {
            // only keep the "TerrainSummer" active at start of play
            Setting1();
        }

        // Update is called once per frame
        void Update()
        {
            // if "1" key pressed 
            if (Input.GetKeyDown(KeyCode.A))
            {
                PostProcessingManager.Instance.FadeToBlack(Setting1);
                //Setting1();
            }
            // if "2" key pressed
            if (Input.GetKeyDown(KeyCode.S))
            {
                PostProcessingManager.Instance.FadeToBlack(Setting2);
                //Setting2();
            }
            // if "3" key pressed
            if (Input.GetKeyDown(KeyCode.D))
            {
                PostProcessingManager.Instance.FadeToBlack(Setting3);
                //Setting3();
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
            House[0].SetActive(true);
            House[1].SetActive(false);
            House[2].SetActive(false);
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
            House[0].SetActive(false);
            House[1].SetActive(true);
            House[2].SetActive(false);
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
            House[0].SetActive(false);
            House[1].SetActive(false);
            House[2].SetActive(true);
        }
    }
}
