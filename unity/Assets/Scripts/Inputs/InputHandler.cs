using System;
using UnityEngine;

namespace Core.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        public Action<Vector3> PressedGround;

        private Terrain mLevelTerrain;
        private int mGroundLayerMask;

        private Vector3 mDownPosition;

        // Use this for initialization
        void Start()
        {
            mLevelTerrain = GetComponent<Terrain>();
            mGroundLayerMask = LayerMask.GetMask("Ground");
        }

        // Update is called once per frame
        void Update()
        {
            if (PressedGround != null)
            {
                mDownPosition = new Vector3();
                if (IsMouseOrTouchDown(ref mDownPosition, mGroundLayerMask))
                {
                    PressedGround(mDownPosition);
                }
            }
        }

        private static bool IsMouseOrTouchDown(ref Vector3 aWorldPosition, int aLayerMask, int aButton = 0)
        {
            var isDown = false;
            Vector2 inputPosition;
            if (Input.touchSupported && Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                isDown = touch.phase == TouchPhase.Began;
                inputPosition = touch.position;
            }
            else
            {
                isDown = Input.GetMouseButtonDown(aButton);
                inputPosition = Input.mousePosition;
            }

            if (isDown)
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(inputPosition);
                if (Physics.Raycast(ray, out hit, 1000F, aLayerMask))
                {
                    aWorldPosition = hit.point;
                }
            }

            return isDown;
        }
    }
}

