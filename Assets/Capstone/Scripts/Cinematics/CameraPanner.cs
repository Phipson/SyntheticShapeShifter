using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace InterDigital.CMU
{
    public class CameraPanner : MonoBehaviour
    {
        [Header("Panner Configurations")]
        public float Duration;

        [Header("Look At Configurations")]
        public Transform LookAtObject;
        public bool LookAt;

        [Header("From To Movement")]
        public Transform From;
        public Transform To;
        public bool MoveCamera;


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