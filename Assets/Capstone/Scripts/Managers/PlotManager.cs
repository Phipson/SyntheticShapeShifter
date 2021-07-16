using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace InterDigital.CMU
{
    [System.Serializable]
    public class SceneStartEvent : UnityEvent { }

    [System.Serializable]
    public struct PlotBranch
    {
        public SceneStartEvent onSceneStart;
        public ImageDisplay[] ImageOverlays;
        public AudioClip[] Dialogues; // TODO: Replace with Aaron's class structure for dialog and subtitles
        public Camera[] CloseUps; 
        public Camera[] Peripherals;
        public Camera[] FirstPerson; 
    }

    [System.Serializable]
    public struct ImageDisplay
    {
        public Material ImageTxt;
        public float Delay;
    }

    public class PlotManager : MonoBehaviour
    {
        // 0 = Neutral
        // 1 = Happy
        // 2 = Horror
        // 3 = Sad
        [Header("SCENE: LRRH walking in the woods, seeing animals, etc.")]
        public PlotBranch[] Scene1;

        [Header("SCENE: LRRH encountering wolf in the woods")]
        public PlotBranch[] Scene2;

        [Header("SCENE: Wolf is in house with grandma")]
        public PlotBranch[] Scene3;

        [Header("SCENE: LRRH is in house with fake grandma")]
        public PlotBranch[] Scene4;

        [Header("SCENE: Huntsman with fake grandma in house")]
        public PlotBranch[] Scene5;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}