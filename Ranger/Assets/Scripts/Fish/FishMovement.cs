using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FishMovement : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement PlayerMovement;
    public Fishing Fishing;
    private NavMeshAgent Agent;

    [SerializeField]
    private BoxCollider _box;

    [SerializeField] LayerMask groundLayer, playerLayer, fishingLayer;

    private Vector3 _destination;
    private bool _isSet;

    private readonly float _range = 7f;

    private readonly float _defaultSpeed = 0.5f;

    private readonly float _sightRange = 4f;
    private bool _baitSpotted;

    public bool IsCaught;

    private int _counter;
    private int _num;

    public GameObject Inventory;
    public InventoryManager Manager;

    public GameObject DayNight;
    public TimeManager TimeManager;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = _defaultSpeed;

        Inventory = GameObject.FindGameObjectWithTag("InventoryManager");
        Manager = Inventory.GetComponent<InventoryManager>();

        Player = GameObject.Find("Player");
        PlayerMovement = Player.GetComponent<PlayerMovement>();

        Fishing = Player.GetComponent<Fishing>();

        DayNight = GameObject.FindGameObjectWithTag("TimeManager");
        TimeManager = DayNight.GetComponent<TimeManager>();

        IsCaught = false;
    }

    void Update()
    {
        _baitSpotted = Physics.CheckSphere(transform.position, _sightRange, fishingLayer);
        SwimAround();
        CheckCatching();

        if (_baitSpotted)
        {
            Agent.SetDestination(new Vector3(GameObject.FindGameObjectWithTag("bait").transform.position.x, transform.position.y, 
                                             GameObject.FindGameObjectWithTag("bait").transform.position.z));
        }
    }

    void SwimAround()
    {
        if (!IsCaught)
        {
            if (!_isSet)
            {
                SearchForDestination();
            }

            if (_isSet)
            {
                Agent.SetDestination(_destination);
            }

            if (Vector3.Distance(transform.position, _destination) < 5)
            {
                _isSet = false;
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bait")
        {
            IsCaught = true;
            _box.size = new Vector3(4.5f, 1.85f, 4.5f);
            if (TimeManager.service.isDayTime.Value)
            {
                _counter = 5;
            }
            else
            {
                _counter = 10;
            }
            _num = Random.Range(0, 3);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }

    private void CheckCatching()
    {
        if (IsCaught)
        {
            _isSet = true;

            if (_counter != 0)
            {
                StartCoroutine(Wait());
                if (Fishing.FishOnce(_num))
                {
                    _counter--;
                    _num = Random.Range(0, 3);
                }
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                _isSet = false;
                _counter = 0;
                Fishing.DisableQuickTimeUI();
                PlayerMovement.IsFishing = false;
                IsCaught = false;
                return;
            }

            if (_counter == 0)
            {
                Fishing.DisableQuickTimeUI();
                PlayerMovement.IsFishing = false;
                Agent.speed = 0;

                transform.position = Vector3.Lerp(transform.position, Player.transform.position, 1f * Time.deltaTime);
            }
        }
    }
}