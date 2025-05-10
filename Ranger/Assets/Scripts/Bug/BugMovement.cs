using UnityEngine;
using UnityEngine.AI;

public class BugMovement : MonoBehaviour
{
    public GameObject Player;
    private NavMeshAgent Agent;

    [SerializeField] LayerMask groundLayer, playerLayer, plantLayer;

    private Vector3 _destination;
    private bool _isSet;
    private float _range = 15;

    private float _defaultSpeed = 5f;
    private float _runningSpeed = 15f;

    private float _sightRange = 5f;
    private bool _playerSpotted;
    private bool _plantSpotted;


    public PlayerMovement PlayerMovement;

    //private GameObject PickupPlant;
    //private float _plantRange = 30f;

    public GameObject Inventory;
    public InventoryManager Manager;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement = Player.GetComponent<PlayerMovement>();

        Agent.speed = _defaultSpeed;

        Inventory = GameObject.FindGameObjectWithTag("InventoryManager");
        Manager = Inventory.GetComponent<InventoryManager>();
        //Plant = GameObject.FindGameObjectWithTag("PlantDropped");
        //InventoryClass = Inventory.GetComponent<Inventory>();
    }

    void Update()
    {

        

        _playerSpotted = Physics.CheckSphere(transform.position, _sightRange, playerLayer);
        _plantSpotted = Physics.CheckSphere(transform.position, 15f, plantLayer);
        //Debug.Log(_plantSpotted);
        //Inventory.SetActive(true);
        //InventoryScript = Inventory.GetComponent<Inventory>();

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

        if (_plantSpotted)
        {
            if (PlayerMovement.IsCrouching)
            {
                PlantLure();
            }
        }

        //Debug.Log(PlayerMovement.IsCrouching);
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
        _destination = new Vector3( transform.position.x + Random.Range(-_range, _range),
                                    transform.position.y,
                                    transform.position.z + Random.Range(-_range, _range));

        //Debug.Log(_isSet);

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
        Collider[] hitPlantsDropped = Physics.OverlapSphere(transform.position, 15f, plantLayer);
        _destination = hitPlantsDropped[0].gameObject.transform.position;
        Agent.SetDestination(_destination);
    }
}