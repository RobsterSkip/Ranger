using UnityEngine;
using UnityEngine.AI;

public class FishMovement : MonoBehaviour
{
    public GameObject Player;
    public PlayerMovement PlayerMovement;
    private NavMeshAgent Agent;

    [SerializeField] LayerMask groundLayer, playerLayer, fishingLayer;

    private Vector3 _destination;
    private bool _isSet;
    private float _range = 7f;

    private float _defaultSpeed = 0.5f;

    private float _sightRange = 4f;
    private bool _baitSpotted;

    public bool IsCaught;
    private int _counter;
    private int _num;


    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        Agent.speed = _defaultSpeed;
        IsCaught = false;
    }

    void Update()
    {
        _baitSpotted = Physics.CheckSphere(transform.position, _sightRange, fishingLayer);
        SwimAround();
        CheckCatching();

        if (_baitSpotted)
        {
            Agent.SetDestination(new Vector3(GameObject.FindGameObjectWithTag("bait").transform.position.x, transform.position.y, GameObject.FindGameObjectWithTag("bait").transform.position.z));
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
            _counter = 5;
            _num = Random.Range(0, 3);
            Destroy(other.gameObject);
        }
    }

    private void CheckCatching()
    {
        if (IsCaught)
        {
            _isSet = true;
            if (_counter != 0)
            {
                if (_num == 0)
                {
                    Debug.Log("<--");
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        _counter--;
                        _num = Random.Range(0, 2);
                    }
                }

                if (_num == 1)
                {
                    Debug.Log("-->");
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        _counter--;
                        _num = Random.Range(0, 2);
                    }
                }

                if (_num == 2)
                {
                    Debug.Log("pull (S)");
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        _counter--;
                        _num = Random.Range(0, 2);
                    }
                }
            }

            if (_counter == 0)
            {
                Debug.Log("congrats!"); //fish caught
                PlayerMovement.IsFishing = false;
                Destroy(gameObject);
            }
        }
    }
}