using Core.Inputs;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject UnitPrefab;
    public GameObject TargetPrefab;

    private Terrain mLevelTerrain;
    private InputHandler mInputHandler;
    private readonly List<UnitController> mSpawnedUnits = new List<UnitController>();
    private GameObject mTargetSpot;

	// Use this for initialization
	void Start()
    {
        mLevelTerrain = GetComponent<Terrain>();
        mInputHandler = GetComponent<InputHandler>();

        mInputHandler.PressedGround += OnInputPressedDown;
    }

    private void OnInputPressedDown(Vector3 aWorldPosition)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!mTargetSpot)
            {
                mTargetSpot = Instantiate(TargetPrefab);
            }
            mTargetSpot.transform.position = aWorldPosition;

            mSpawnedUnits.ForEach((unit) => unit.Target = mTargetSpot.transform);
        }
        else
        {
            var unit = Instantiate(UnitPrefab, aWorldPosition, Quaternion.identity);
            mSpawnedUnits.Add(unit.GetComponent<UnitController>());
        }
    }
}
