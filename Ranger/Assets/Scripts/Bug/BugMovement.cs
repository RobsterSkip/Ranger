using UnityEngine;
using UnityEngine.AI;

public class BugMovement : MonoBehaviour
{
    public GameObject Player;
    private NavMeshAgent Agent;

    [SerializeField] LayerMask groundLayer, playerLayer, plantLayer;

    private Vector3 _destination;
    private bool _isSet;
    private readonly float _range = 15;

    private readonly float _defaultSpeed = 5f;
    private readonly float _runningSpeed = 15f;
    
    private readonly float _daySpeedMultiplier = 1f;
    private readonly float _nightSpeedMultiplier = 5f;
    private float _currentMultiplier;

    private readonly float _sightRange = 12f;
    private bool _playerSpotted;
    private bool _plantSpotted;

    public PlayerMovement PlayerMovement;

    public GameObject Inventory;
    public InventoryManager Manager;

    public GameObject DayNight;
    public TimeManager TimeManager;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement = Player.GetComponent<PlayerMovement>();

        Agent.speed = _defaultSpeed * _currentMultiplier;

        Inventory = GameObject.FindGameObjectWithTag("InventoryManager");
        Manager = Inventory.GetComponent<InventoryManager>();

        DayNight = GameObject.FindGameObjectWithTag("TimeManager");
        TimeManager = DayNight.GetComponent<TimeManager>();
    }

    void Update()
    {
        if(TimeManager.service.isDayTime.Value)
        {
            _currentMultiplier = _daySpeedMultiplier;
        }
        else
        {
            _currentMultiplier = _nightSpeedMultiplier;
        }
        

        _playerSpotted = Physics.CheckSphere(transform.position, _sightRange, playerLayer);
        _plantSpotted = Physics.CheckSphere(transform.position, 7f, plantLayer);

        WalkAround();

        if (_playerSpotted )
        {
            if (!PlayerMovement.IsCrouching)
            {
                Agent.speed = _runningSpeed * _currentMultiplier;
                RunAway();
            }
        }
        else
        {
            Agent.speed = _defaultSpeed * _currentMultiplier;
        }

        if (_plantSpotted)
        {
            if (PlayerMovement.IsCrouching)
            {
                PlantLure();
            }
        }
    }

    void WalkAround()
    {
        if (Agent.velocity == Vector3.zero)
        {
            _isSet = false;
        }

        if (!_isSet)
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

        if (Physics.Raycast(_destination, Vector3.down, groundLayer))
        {
            _isSet = true;
        }
    }

    void RunAway()
    {
        _destination = transform.position + (transform.position - Player.transform.position);
        Agent.SetDestination(_destination);
    }

    void PlantLure()
    {
        Collider[] hitPlantsDropped = Physics.OverlapSphere(transform.position, 20f, plantLayer);
        _destination = hitPlantsDropped[0].gameObject.transform.position;
        Agent.SetDestination(_destination);
    }
}