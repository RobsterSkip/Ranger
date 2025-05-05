using UnityEngine;
using UnityEngine.AI;

public class FishMovement : MonoBehaviour
{
    public GameObject Player;
    private NavMeshAgent Agent;

    [SerializeField] LayerMask groundLayer, playerLayer;

    private Vector3 _destination;
    private bool _isSet;
    private float _range = 7f;

    private float _defaultSpeed = 0.5f;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        Agent.speed = _defaultSpeed;
    }

    void Update()
    {
        SwimAround();
    }

    void SwimAround()
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
        _destination = new Vector3( transform.position.x + Random.Range(-_range, _range),
                                    transform.position.y,
                                    transform.position.z + Random.Range(-_range, _range));

        if(Physics.Raycast(_destination, Vector3.down, groundLayer))
        {
            _isSet = true;
        }
    }
}