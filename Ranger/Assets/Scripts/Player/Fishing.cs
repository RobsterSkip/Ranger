using System.Runtime.CompilerServices;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public PlayerMovement PlayerMovement;

    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Debug.Log(PlayerMovement.CanFish);
    }
}