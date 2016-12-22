using UnityEngine;

namespace Gameplay.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public float Speed = 5F;
        public int ScreenBoundaries = 50;
        public float ZoomScale = 2.5F;
        public float MinimumZoomDistance = 75F;
        public float MaximumZoomDistance = 200F;
        public bool HandleKeyboard = true;
        public bool HandleMouse = true;

        [Range(0, 1)]
        [SerializeField]
        private float m_ZoomPercent = 1.0F;

        private Camera mCamera;
        private Terrain mLevelTerrain;
        private float mZoomDistance;

        public float Zoom { get { return m_ZoomPercent; } set { m_ZoomPercent = value; OnValidate(); } }

        void Start()
        {
            mCamera = GetComponent<Camera>();
            mLevelTerrain = FindObjectOfType<Terrain>();

            mZoomDistance = mCamera.transform.position.y;

            OnValidate();
        }

        void Update()
        {
            Vector3 move;
            float zoomDelta;
            if (GetInputMove(out move))
            {
                MoveWithinScreenLimits(ref move);
                MoveWithinLevelLimits(ref move);
                transform.Translate(move, Space.World);
            }
            if (GetZoomMove(out zoomDelta))
            {
                ZoomWithinLimits(ref zoomDelta);
                transform.position = GetZoomedPosition(zoomDelta, ZoomScale);
                mZoomDistance = transform.position.y;
                m_ZoomPercent = GetZoomPercent();
            }
        }

        void OnValidate()
        {
            if (mCamera == null) return;

            var previousDistance = mZoomDistance;
            mZoomDistance = Mathf.Lerp(MinimumZoomDistance, MaximumZoomDistance, m_ZoomPercent);
            //m_ZoomPercent = GetZoomPercent();

            mCamera.transform.position = GetZoomedPosition(previousDistance - mZoomDistance, 1); //new Vector3(mCamera.transform.position.x, mZoomDistance, mCamera.transform.position.z);
        }

        private bool GetInputMove(out Vector3 aMove)
        {
            aMove = Vector3.zero;
            var hasToMove = false;

            if (HandleMouse)
            {
                if (Input.mousePosition.x > Screen.width - ScreenBoundaries)
                {
                    aMove.x = Speed;
                    hasToMove = true;
                }
                else if (Input.mousePosition.x < ScreenBoundaries)
                {
                    aMove.x = -Speed;
                    hasToMove = true;
                }

                if (Input.mousePosition.y < ScreenBoundaries)
                {
                    aMove.z = -Speed;
                    hasToMove = true;
                }
                else if (Input.mousePosition.y > Screen.height - ScreenBoundaries)
                {
                    aMove.z = Speed;
                    hasToMove = true;
                }
            }

            if (HandleKeyboard)
            {
                if (Input.GetKey("right"))
                {
                    aMove.x = Speed;
                    hasToMove = true;
                }
                else if (Input.GetKey("left"))
                {
                    aMove.x = -Speed;
                    hasToMove = true;
                }

                if (Input.GetKey("down"))
                {
                    aMove.z = -Speed;
                    hasToMove = true;
                }
                else if (Input.GetKey("up"))
                {
                    aMove.z = Speed;
                    hasToMove = true;
                }
            }

            return hasToMove;
        }

        private void MoveWithinScreenLimits(ref Vector3 aMove)
        {
            if (Input.mousePosition.x > Screen.width || Input.mousePosition.x < 0)
            {
                aMove.x = 0;
            }
            if (Input.mousePosition.y > Screen.height || Input.mousePosition.y < 0)
            {
                aMove.z = 0;
            }
        }

        private void MoveWithinLevelLimits(ref Vector3 aMove)
        {

        }

        private bool GetZoomMove(out float aZoomDelta)
        {
            aZoomDelta = 0;
            var wheelDelta = Input.mouseScrollDelta.y;

            if (wheelDelta != 0)
            {
                aZoomDelta = wheelDelta;

                return true;
            }
            return false;
        }

        private void ZoomWithinLimits(ref float aZoomDelta)
        {
            var previewZoom = GetZoomedPosition(aZoomDelta, ZoomScale);

            if (previewZoom.y > MaximumZoomDistance || previewZoom.y < MinimumZoomDistance)
            {
                aZoomDelta = 0;
            }
        }

        private Vector3 GetZoomedPosition(float aZoomDelta, float aScale)
        {
            return transform.position + transform.forward * (aZoomDelta * ZoomScale);
        }

        private float GetZoomPercent()
        {
            return (mZoomDistance - MinimumZoomDistance) / (MaximumZoomDistance - MinimumZoomDistance);
        }
    }
}
