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
    private float _runningSpeed = 15f;

    private float _sightRange = 5f;
    private bool _playerSpotted;

    public PlayerMovement PlayerMovement;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        Agent.speed = _defaultSpeed;
    }

    void Update()
    {
        _playerSpotted = Physics.CheckSphere(transform.position, _sightRange, playerLayer);
        //Debug.Log(_playerSpotted);
        
        WalkAround();

        if (_playerSpotted )
        {
            if (!PlayerMovement.IsCrouching)
            {
                Agent.speed = _runningSpeed;
                RunAway();
            }
        }
        else
        {
            Agent.speed = _defaultSpeed;
        }
        
        Debug.Log(PlayerMovement.IsCrouching);
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

    void RunAway()
    {
        _destination = transform.position + (transform.position - Player.transform.position);
        Agent.SetDestination(_destination);
    }
}