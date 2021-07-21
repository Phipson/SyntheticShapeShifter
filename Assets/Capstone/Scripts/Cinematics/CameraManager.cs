using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace InterDigital.CMU
{
    public enum CameraType
    {
        closeUp,
        lowAngle,
        topDown,
        neutral
    }

    public class CameraManager : MonoBehaviour
    {
        [Header("Cameras")]
        public Camera[] CloseUpCameras;
        public Camera[] LowAngleCameras;
        public Camera[] TopDownCameras;
        public Camera[] NeutralCameras;

        CameraType cType = CameraType.neutral;
        Dictionary<CameraType, Camera[]> CameraEmotionMap = new Dictionary<CameraType, Camera[]>();
        int CameraIndex = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void PlayNextCamera(Camera cam, CameraType newType)
        {
            if (cType != newType)
                CameraIndex = 0;

            cam.enabled = false;
            Camera newCam = CameraEmotionMap[cType][CameraIndex];
            newCam.enabled = true;

            CameraIndex = CameraIndex < CameraEmotionMap[cType].Length - 1 ? CameraIndex + 1 : 0;
            cType = newType;
        }
    }
}