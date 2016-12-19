using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed = 5F;
    public int ScreenBoundaries = 50;
    public float ZoomScale = 2.5F;
    public float MinimumZoomDistance = 75F;
    public float MaximumZoomDistance = 200F;

    private Camera mCamera;
    private Terrain mLevelTerrain;
    private float mZoomDistance;

    void Start ()
    {
        mCamera = GetComponent<Camera>();
        mLevelTerrain = FindObjectOfType<Terrain>();

        mZoomDistance = mCamera.transform.position.y;
	}
	
	void Update ()
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
        }
    }

    private bool GetInputMove(out Vector3 aMove)
    {
        aMove = Vector3.zero;
        var hasToMove = false;
        if (Input.mousePosition.x > Screen.width - ScreenBoundaries || Input.GetKey("right"))
        {
            aMove.x = Speed;
            hasToMove = true;
        }
        else if (Input.mousePosition.x < ScreenBoundaries || Input.GetKey("left"))
        {
            aMove.x = -Speed;
            hasToMove = true;
        }

        if (Input.mousePosition.y < ScreenBoundaries || Input.GetKey("down"))
        {
            aMove.z = -Speed;
            hasToMove = true;
        }
        else if (Input.mousePosition.y > Screen.height - ScreenBoundaries || Input.GetKey("up"))
        {
            aMove.z = Speed;
            hasToMove = true;
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

        if (previewZoom.y > MaximumZoomDistance ||
            previewZoom.y < MinimumZoomDistance)
        {
            aZoomDelta = 0;
        }
    }

    private Vector3 GetZoomedPosition(float aZoomDelta, float aScale)
    {
        return transform.position + transform.forward * (aZoomDelta * ZoomScale);
    }
}
