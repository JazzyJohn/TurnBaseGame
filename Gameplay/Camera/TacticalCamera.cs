using UnityEngine;
using System.Collections;
using InputLogic;

namespace CameraLogic
{
    public class TacticalCamera : MonoBehaviour
    {
        Camera mainCamera;
        public Transform cameraRoot;
        public Vector3 maxOffsetFromRoot;
        public Vector3 minOffsetFromRoot;
        Vector3 offsetFromRoot;
        public LayerMask groundmaskForCamera;
        // Use this for initialization
        void Awake()
        {
            mainCamera = Camera.main;
            mainCamera.transform.parent = cameraRoot;
            offsetFromRoot = maxOffsetFromRoot;
            mainCamera.transform.localPosition = offsetFromRoot;
            mainCamera.transform.LookAt(cameraRoot);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 rootPosition = cameraRoot.position;
            Vector3 moveDelta = InputManager.GetCameraMoveAxis() * Time.deltaTime;

            float hDelta = moveDelta.y;
            moveDelta.y = 0;
            rootPosition += cameraRoot.rotation * moveDelta;
            cameraRoot.position = rootPosition;

            offsetFromRoot.y = Mathf.Clamp(offsetFromRoot.y + hDelta, minOffsetFromRoot.y, maxOffsetFromRoot.y);

            mainCamera.transform.localPosition = offsetFromRoot;

            float rotation = InputManager.GetCameraRotation() * Time.deltaTime;

            cameraRoot.rotation = Quaternion.Euler(0, rotation, 0) * cameraRoot.rotation;
        }
    }
}
