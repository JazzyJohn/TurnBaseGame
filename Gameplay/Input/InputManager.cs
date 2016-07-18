using UnityEngine;
using System.Collections;
using GameFlow;
using UnityEngine.EventSystems;

namespace InputLogic
{
    public class InputManager : MonoBehaviour
    {
        static InputManager s_instance;
        Vector3 cameraMoveAxis;
        float cameraRotation = 0.0f;
        int cameraLvl = 0;
        public float edgeSizeforMouse = 2.0f;
        public float scrollSensativite = 1.0f;
        public float zoomSensativite = 1.0f;
        public float rotationSensativite = 1.0f;
        public string horizontalAxisName = "Horizontal";
        public string verticalAxisName = "Vertical";
        public string rotateCameraBtn = "rotateCamera";
        OrderControl orderControl;
       
        // Use this for initialization
        void Awake()
        {
            s_instance = this;
            orderControl = GetComponent<OrderControl>();
           
        }

        // Update is called once per frame
        void Update()
        {
            CalculateCameraMove();
            ResolveClick();
        }

        private void ResolveClick()
        {
            if(Input.GetMouseButtonDown(0) && !IsMouseOverUI())
            {
                orderControl.Click();
            }
            if (Input.GetMouseButtonDown(1) && !IsMouseOverUI())
            {
                orderControl.RightClick();
            }
        }

        void CalculateCameraMove()
        {
            Vector3 mousepos = Input.mousePosition;
            cameraMoveAxis = Vector3.zero;
            cameraRotation = 0;
            if (Input.GetButton(rotateCameraBtn))
            {
                if(Input.GetAxis(horizontalAxisName)>0)
                {
                    cameraRotation = rotationSensativite;
                }
                else if(Input.GetAxis(horizontalAxisName)<0)
                {
                    cameraRotation = -rotationSensativite;
                }
            }
            else
            {

                if (mousepos.x < edgeSizeforMouse || Input.GetAxis(horizontalAxisName) < 0)
                {
                    cameraMoveAxis.x = -scrollSensativite;
                }
                else if (mousepos.x > Screen.width - edgeSizeforMouse || Input.GetAxis(horizontalAxisName) > 0)
                {
                    cameraMoveAxis.x = scrollSensativite;
                }

                if (mousepos.y < edgeSizeforMouse || Input.GetAxis(verticalAxisName) < 0)
                {
                    cameraMoveAxis.z = -scrollSensativite;
                }
                else if (mousepos.y > Screen.height - edgeSizeforMouse || Input.GetAxis(verticalAxisName) > 0)
                {
                    cameraMoveAxis.z = scrollSensativite;
                }
            }
            cameraMoveAxis.y = Input.GetAxis("Mouse ScrollWheel") * zoomSensativite;
        }
        

        public static Vector3 GetCameraMoveAxis()
        {
            return s_instance.cameraMoveAxis;
        }

        public static float GetCameraRotation()
        {
            return s_instance.cameraRotation;
        }

        public static int GetCamLvl()
        {
            return s_instance.cameraLvl;
        }

        public static void EndTurn()
        {
            GameFlowController.EndTurn();
        }

        public static void StartAction(int p)
        {
            s_instance.orderControl.DoAction(p);
        }

        public bool IsMouseOverUI()
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {

                return true;
            }
            return false;
        }
    }
}
