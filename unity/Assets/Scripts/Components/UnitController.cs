using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    public float Speed = 2;
    [HideInInspector]
    public Transform Target;

    private NavMeshAgent mAgent;

	// Use this for initialization
	void Start ()
    {
        mAgent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Target)
        {
            mAgent.SetDestination(Target.position);
        }
	}
}
