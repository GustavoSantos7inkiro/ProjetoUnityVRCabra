using UnityEngine;

public class PlayerCollisionTest : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "FloorCollider")
        {
            Debug.Log("Player tocou no chão da casa!");
        }
    }
}