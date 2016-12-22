using Core.Units.Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [HideInInspector]
    public Transform Target;

    private NavMeshAgent mAgent;

	// Use this for initialization
	void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update()
    {
        if (Target)
        {
            mAgent.SetDestination(Target.position);
        }
    }

    public void ApplySettingsFrom(IMoveableItem aItem)
    {
        mAgent.speed = aItem.Speed;
        mAgent.updateRotation = aItem.CanRotate;
    }
}
