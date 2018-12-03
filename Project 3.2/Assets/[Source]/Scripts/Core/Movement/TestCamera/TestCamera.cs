using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Interactions;
using UnityEngine.Experimental.Input.Plugins.UI;
using UnityEngine.Experimental.Input.Plugins.Users;
using UnityEngine.Experimental.Input.Utilities;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;

namespace Core.Movement
{
    [RequireComponent(typeof(Camera))]
    public class TestCamera : MonoBehaviour, IGameplayActions
    {

        #region Variables

            public bool useFixedUpdate = false;

            public bool useScrollPanning = true;

            public bool useScreenEdgeInput = true;

            public float screenEdgeBorder = 2f;

            public bool useKeyboardInput = true;
            //public string horizontalAxis = "Horizontal";
            //public string verticalAxis = "Vertical";

            public bool usePanning = true;
            //public KeyCode panningKey = KeyCode.Mouse2;

            public bool useKeyboardZooming = true;

            //public KeyCode zoomInKey = KeyCode.R;
            //public KeyCode zoomOutKey = KeyCode.F;

            public bool useScrollwheelZooming = true;
            public bool invertScrollwheel = false;
            //public string zoomingAxis = "Mouse ScrollWheel";

            public bool rotateCameraHorizontal = false;
            public bool moveCameraTowardsMouse = false;

            public bool useKeyboardRotation = true;
            //public KeyCode rotateRightKey = KeyCode.E;
            //public KeyCode rotateLeftKey = KeyCode.Q;

        
            //[FormerlySerializedAs("useMouseRotation")] public bool UseMouseRotation = true;
            public bool useMouseRotation = true;
            //public KeyCode mouseRotationKey = KeyCode.LeftAlt;

            //public Vector3 collisionChecker;

            #region Serialized
    
            #region Input
    
            /// <summary>
            /// Controls used by this player.
            /// </summary>
            public PrototypeControlmap controlmap;
    
            #endregion
    
            #region Movement
    
            [FoldoutGroup("Movement", expanded: false)]
            [SerializeField] float keyboardMoveSpeed = 20f, edgeScrollSpeed = 10f;
    
            //[SerializeField] float panningSpeed = 40f;
            [FoldoutGroup("Movement")]
            [SerializeField] float followingSpeed = 5f;
    
            [FoldoutGroup("Movement")]
            [SerializeField] float keyboardRotationSpeed = 25f, mouseRotationSpeed = 10f;
    
            #endregion
    
            #region Height
    
            [FoldoutGroup("Height", expanded: false)]
            [SerializeField] bool autoHeight = true;
    
            [FoldoutGroup("Height"), PropertyTooltip("Layermask of ground and/or other objects that affect height")]
            [SerializeField] LayerMask groundMask = -1;
    
            [FoldoutGroup("Height"), HorizontalGroup("Height/Height Clamp", width: 0.5f), MinValue(0)]
            [SerializeField] float maxHeight = 100f, minHeight = 15f;
    
            [FoldoutGroup("Height"), HorizontalGroup("Height/Pitch Rotation", width: 0.5f)]
            [SerializeField] float maxPitchRotation = 90f, minPitchRotation = 45f;
    
            [PropertyTooltip("Curve that adjusts pitch rotation based on height")]
            [SerializeField] AnimationCurve pitchCurve; //= new AnimationCurve(;
    
            [SerializeField] float zoomSpeed = 20f;
            [PropertyTooltip("Curve that adjusts zooming speed based on height")]
            [SerializeField] AnimationCurve zoomCurve;
    
            [SerializeField] float heightDampening = 2f;
    
            [SerializeField] float keyboardZoomingSensitivity = 2f;
            [SerializeField] float scrollwheelZoomingSensitivity = 25f;
    
            //private float zoomPos = 0;
    
            #endregion
    
            #region MapLimits
    
            [FoldoutGroup("Map Limits", expanded: false)]
            [SerializeField] bool limitMap = false;
            [FoldoutGroup("Map Limits")]
            [SerializeField] Transform border = null; //limit map using a cube (Does NOT work with rotations)
    
            [FoldoutGroup("Map Limits")]
            [SerializeField] float limitX = 500f, limitY = 500f;
    
            #endregion
    
            #region Targeting
    
            [SerializeField] bool targetFollowing = false; //EDIT: REMOVE THIS, will be replaced by Cinemachine
    
            [SerializeField] Transform targetFollow; //target to follow
            [SerializeField] Vector3 targetOffset; 
    
            /// <summary>
            /// Are we following our target?
            /// </summary>
            private bool FollowingTarget
            {
                get
                {
                    return targetFollow != null;
                }
            }
    
            #endregion
    
            #endregion
    
            #region Non-Serialized
    
            #region Input
    
            private Vector2 MoveInput{ get; set; }
    
            private Vector2 MouseInput
            {
                get
                {
                    return Input.mousePosition;
                }
            }
    
            private float KeyboardZoomInput
            {
                get; set;
            }
    
            private float ScrollwheelZoomInput
            {
                get; set;
            }
    
            private float YawRotation
            {
                get; set;
            }
    
            private bool MouseRotationToggled
            {
                get; set;
            }
    
            private Vector3 xDirection = Vector3.zero, yDirection = Vector3.zero;
    
            #endregion
    
            private bool isInMenu = false;
    
            private float deltaTime;
    
            private bool debugging;
    
    
            private float zoomPercentage;
    
            //private Vector3 zoomTarget;
    
            private Vector3 fitted_WorldPos, rel_WorldPos;
    
            /*
            public float DeltaTime
            {
                get
                {
                    if (!useFixedUpdate)
                    {
                        return Time.fixedDeltaTime;
                    }
                    else
                    {
                        return Time.deltaTime;
                    }
                }
            }
            */
    
            #endregion

        #endregion

        #region Unity_Methods

        private void Awake()
        {
            Debug.Assert(controlmap != null);
        }

        public void OnEnable()
        {
            controlmap.Enable();
        }
        public void OnDisable()
        {
            controlmap.Disable();
        }

        private void StartGame()
        {
            controlmap.gameplay.Enable();
        }

        private void Update()
        {
            if (!useFixedUpdate)
            {
                deltaTime = Time.deltaTime;
                UpdateCamera();
            }
        }
        private void FixedUpdate()
        {
            if (useFixedUpdate)
            {
                deltaTime = Time.fixedDeltaTime;
                UpdateCamera();
            }
        }

        private void OnDrawGizmos()
        {
            if (debugging)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, 1);

                Gizmos.color = Color.green;
                //Gizmos.DrawWireSphere(GlobalVariables.MouseWorldPosition(), 1);

                //Gizmos.color = Color.yellow;
                //Gizmos.DrawWireSphere(fitted_WorldPos, 1);

                //Gizmos.color = Color.red;
                //Gizmos.DrawWireSphere(fitted_WorldPos, 1);
            }
        }

        #endregion

        #region TestCamera_Methods

        #region InputActions

        public void Reset()
        {
            MoveInput = Vector2.zero;
        }

        public void OnMenu(InputAction.CallbackContext context)
        {
            if (isInMenu)
            {
                // Leave menu.

                //this.ResumeHaptics();

                controlmap.gameplay.Enable();
                controlmap.menu.Disable(); //REVIEW: this should likely be left to the UI input module

                //menuUI.SetActive(false);
            }
            else
            {
                // Enter menu.

                //this.PauseHaptics();

                controlmap.gameplay.Disable();
                controlmap.menu.Enable();///REVIEW: this should likely be left to the UI input module

                // We do want the menu toggle to remain active. Rather than moving the action to its
                // own separate action map, we just go and enable that one single action from the
                // gameplay actions.
                // NOTE: This will cause gameplay.enabled to remain true.
                // NOTE: This setup won't work on Steam where we can only have a single action set active
                //       at any time. We ignore the gameplay/menu action on Steam and instead handle
                //       menu toggling via the two separate actions gameplay/steamEnterMenu and menu/steamExitMenu
                //       that we use only for Steam.

                controlmap.gameplay.Menu.Enable();

                //menuUI.SetActive(true);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
            Debug.Log(context.control.name);

            if (context.control.name == "Keyboard")
            {
                KeyboardZoomInput = context.ReadValue<float>();
            }

            if(context.control.name == "scroll/x [Mouse]")
            {
                ScrollwheelZoomInput = context.ReadValue<float>();
            }


            //ZoomInput = context.ReadValue<float>();
        }

        public void OnRotateYaw(InputAction.CallbackContext context)
        {
            YawRotation = context.ReadValue<float>();
        }

        public void OnToggleMouseRotation(InputAction.CallbackContext context)
        {
            MouseRotationToggled = context.ReadValue<bool>();
        }

        public void OnPrimaryInteract(InputAction.CallbackContext context)
        {

        }

        public void OnSecondaryInteract(InputAction.CallbackContext context)
        {

        }

        #endregion

        /// <summary>
        /// Update camera
        /// </summary>
        private void UpdateCamera()
        {
            //if (FollowingTarget)
            //{
            //    FollowTarget();
            //}
            //else
            //{
            //    Movement();
            //    Rotation();
            //}

            //HeightCalculation();
            //LimitPosition();
            Movement();
            HeightCalculation();
            Rotation();
            LimitPosition();
        }


        /// <summary>
        /// Move camera with keyboard or with screen edge
        /// </summary>
        private void Movement()
        {
            if (useKeyboardInput)
            {
                Vector3 desiredMove = new Vector3(MoveInput.x, 0, MoveInput.y);

                desiredMove *= keyboardMoveSpeed;
                desiredMove *= deltaTime;
    
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = transform.InverseTransformDirection(desiredMove);

                transform.Translate(desiredMove, Space.Self);
            }

            if (useScreenEdgeInput)
            {
                // determine directions the camera should move in when scrolling
                yDirection.x = transform.up.x;
                yDirection.y = 0;
                yDirection.z = transform.up.z;
                yDirection = yDirection.normalized;

                xDirection.x = transform.right.x;
                xDirection.y = 0;
                xDirection.z = transform.right.z;
                xDirection = xDirection.normalized;

                Vector3 targetMove = new Vector3();

                if (MouseInput.x >= Screen.width - screenEdgeBorder)
                {
                    targetMove.x = targetMove.x + xDirection.x;
                    targetMove.z = targetMove.z + xDirection.z;
                }
                if (MouseInput.x <= screenEdgeBorder)
                {
                    targetMove.x = targetMove.x - xDirection.x;
                    targetMove.z = targetMove.z - xDirection.z;
                }

                if (MouseInput.y >= Screen.height - screenEdgeBorder)
                {
                    targetMove.x = targetMove.x + yDirection.x;
                    targetMove.z = targetMove.z + yDirection.z;
                }
                if (MouseInput.y <= screenEdgeBorder)
                {
                    targetMove.x = targetMove.x - yDirection.x;
                    targetMove.z = targetMove.z - yDirection.z;
                }

                targetMove *= edgeScrollSpeed;
                targetMove *= deltaTime;

                //targetMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * targetMove;
                //targetMove = transform.InverseTransformDirection(targetMove);

                transform.Translate(targetMove, Space.Self);
            }       
        
            /*
            if(usePanning && Input.GetKey(panningKey) && MouseAxis != Vector2.zero) //Uses 
            {
                Vector3 desiredMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);

                desiredMove *= panningSpeed; //EDIT: NO MORE PANNING SPEED!!
                desiredMove *= deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
                desiredMove = transform.InverseTransformDirection(desiredMove);

                transform.Translate(desiredMove, Space.Self);
            }
            */

        }

        /// <summary>
        /// Calculate height & zooming
        /// </summary>
        private void HeightCalculation()
        {
            float distanceToGround = DistanceToGround();

            if (autoHeight)
            {
                //collisionChecker = 
            }

            if (useKeyboardZooming)
            {
                zoomPercentage += KeyboardZoomInput * deltaTime * keyboardZoomingSensitivity;
            }
            if (useScrollwheelZooming)
            {
                zoomPercentage += ScrollwheelZoomInput * deltaTime * -scrollwheelZoomingSensitivity;
            }

            zoomPercentage = Mathf.Clamp01(zoomPercentage);

            float targetHeight = Mathf.Lerp(minHeight, maxHeight, zoomPercentage);

            float difference = 0;

            if (distanceToGround != targetHeight)
            {
                difference = targetHeight - distanceToGround; //adding this to targetHeight will make it relative to the ground
            }

            transform.position = new Vector3(transform.position.x, targetHeight + difference, transform.position.z);

            float heightPercentage = (targetHeight - minHeight) / ((maxHeight - minHeight) / 100); //(transform.position.y - difference) //EDIT MAKE COMPATIBLE WITH CHANGING GROUND & BASE ON DISTANCE FROM GROUND

            if (moveCameraTowardsMouse)
            {
                if (Input.GetAxis("Mouse X") >= 0.25f || Input.GetAxis("Mouse X") <= -0.25f || Input.GetAxis("Mouse Y") >= 0.25f || Input.GetAxis("Mouse Y") <= -0.25f)
                {
                    Vector3 m_WorldPos = GlobalVariables.MouseWorldPosition();
                    Vector3 m_AdjustedWorldPos = m_WorldPos; //EDIT Based on height
                    rel_WorldPos = new Vector3(m_AdjustedWorldPos.x, m_AdjustedWorldPos.y + (m_AdjustedWorldPos.y - distanceToGround), m_AdjustedWorldPos.z);

                    Vector3 dir = rel_WorldPos - transform.position;
                    Vector3 rel_Dir = new Vector3(dir.x, m_WorldPos.y, dir.z);

                    float theta = -Mathf.Atan2(rel_Dir.normalized.z, rel_Dir.normalized.x);
                    float adjacent = minHeight * Mathf.Tan(minPitchRotation * Mathf.Deg2Rad); //EDIT HEIGHT RAYCAST

                    float x2 = rel_WorldPos.x - Mathf.Cos(theta) * adjacent;
                    float y2 = rel_WorldPos.z + Mathf.Sin(theta) * adjacent;

                    fitted_WorldPos = new Vector3(x2, rel_WorldPos.y, y2);
                }

                if (ScrollwheelZoomInput >= 0f)
                {
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(fitted_WorldPos.x, transform.position.y, fitted_WorldPos.z),
                            ScrollwheelZoomInput * 100f * deltaTime); //EDIT 100f
                }
                
            }

            if (rotateCameraHorizontal)
            {
                float targetRot = Mathf.Lerp(minPitchRotation, maxPitchRotation, pitchCurve.Evaluate(heightPercentage * 0.01f));
                transform.eulerAngles = new Vector3(targetRot, transform.eulerAngles.y, transform.eulerAngles.z);
            }

        }

        /// <summary>
        /// Rotates the camera
        /// </summary>
        private void Rotation()
        {
            if (useKeyboardRotation)
            {
                transform.Rotate(Vector3.up, YawRotation * deltaTime * keyboardRotationSpeed, Space.World); //RotationDirection
            }

            if (useMouseRotation && MouseRotationToggled)
            {
                transform.Rotate(Vector3.up, -MouseInput.x * deltaTime * mouseRotationSpeed, Space.World); //MouseAxis
            }
        }

        /// <summary>
        /// follow targetif target != null
        /// </summary>
        private void FollowTarget()
        {
            Vector3 targetPos = new Vector3(targetFollow.position.x, transform.position.y, targetFollow.position.z) + targetOffset;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, deltaTime * followingSpeed);
        }

        /// <summary>
        /// Limit camera movement
        /// </summary>
        private void LimitPosition()
        {
            if (!limitMap)
            {
                return;
            }

            Vector3 borderSize = border.lossyScale / 2;
            Vector3 borderPos = border.position;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, (-borderSize.x) + borderPos.x, borderSize.x + borderPos.x),
                transform.position.y,
                    Mathf.Clamp(transform.position.z, (-borderSize.z) + borderPos.z, borderSize.z + borderPos.z));
        }

        /// <summary>
        /// set the target
        /// </summary>
        /// <param name="target"></param>
        public void SetTarget(Transform target)
        {
            targetFollow = target;
        }

        /// <summary>
        /// reset the target (target is set to null)
        /// </summary>
        public void ResetTarget()
        {
            targetFollow = null;
        }

        /// <summary>
        /// calculates distance to ground
        /// </summary>
        private float DistanceToGround()
        {
            Vector3 pos = transform.position;
            Vector3 upperVector = new Vector3(pos.x, pos.y + maxHeight, pos.z);
            Ray ray = new Ray(upperVector, Vector3.down);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, groundMask.value))
            {
                return (hit.point - pos).magnitude;
            }

            return 0f;
        }


        [ContextMenu("Toggle Debugging")]
        private void ToggleDebugging()
        {
            debugging = !debugging;
            //return debugging;
        }

            /*
            /// <summary> 
            /// gets position of mouse in worldspace.
            /// </summary>
            private Vector3 MouseWorldPosition()
            {
                Ray ray = Camera.main.ScreenPointToRay(MouseInput);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, groundMask.value))
                {
                    return (hit.point);
                }

                return Vector3.zero;
            }
            */

            #endregion

    }
}