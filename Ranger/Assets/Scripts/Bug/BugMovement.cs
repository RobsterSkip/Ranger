using UnityEngine;
using UnityEngine.AI;

public class BugMovement : MonoBehaviour
{
    public GameObject Player;
    private NavMeshAgent Agent;

    [SerializeField] LayerMask groundLayer, playerLayer;

    private Vector3 _destination;
    private bool _isSet;
    private float _range = 15;

    private float _defaultSpeed = 5f;
    private float _runningSpeed = 10f;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = _defaultSpeed;
    }

    void Update()
    {
        WalkAround();
    }

    void WalkAround()
    {
        if(!_isSet)
        {
            SearchForDestination();
        }

        if(_isSet)
        {
            Agent.SetDestination(_destination);
        }

        if (Vector3.Distance(transform.position, _destination) < 5)
        {
            _isSet = false;
        }
    }

    void SearchForDestination()
    {
        float Z = Random.Range(-_range,_range);

        _destination = new Vector3( transform.position.x + Random.Range(-_range, _range),
                                    transform.position.y,
                                    transform.position.z + Random.Range(-_range, _range));

        if(Physics.Raycast(_destination, Vector3.down, groundLayer))
        {
            _isSet = true;
        }
    }
}
