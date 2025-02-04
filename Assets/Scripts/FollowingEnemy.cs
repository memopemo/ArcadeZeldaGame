using UnityEngine;

public class FollowingEnemy : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 3);
        transform.LookAt(player.transform);
    }
}
