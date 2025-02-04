using System.Collections;
using UnityEngine;

//Fires at player with a line renderer
public class SniperEnemy : MonoBehaviour
{
    LineRenderer lineRenderer;
    Player player;
    enum State { Aiming, Firing, Fire, Reloading }
    State state;


    // Start is called before the first frame update
    // Start can also be a co-routine!
    IEnumerator Start()
    {
        player = FindFirstObjectByType<Player>();
        lineRenderer = GetComponent<LineRenderer>();
        while (true)
        {
            state = State.Aiming;
            yield return new WaitForSeconds(3);

            state = State.Firing;
            yield return new WaitForSeconds(0.1f);

            state = State.Fire;
            lineRenderer.startWidth = 0.5f;
            lineRenderer.startColor = Color.white;

            //raycast to hit player
            foreach (var item in Physics.RaycastAll(transform.position, lineRenderer.GetPosition(1) - transform.position))
            {
                if (item.collider.TryGetComponent(out Player player))
                {
                    player.Die();
                }
            }
            yield return new WaitForSeconds(1);

            state = State.Reloading;
            yield return new WaitForSeconds(1.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Aiming:
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                lineRenderer.SetPosition(0, transform.position);
                //slowly target the player (not directly though)
                lineRenderer.SetPosition(1, Vector3.Lerp(lineRenderer.GetPosition(1), player.transform.position, Time.deltaTime * 5));
                lineRenderer.startWidth = 0.2f;
                lineRenderer.endWidth = 0.2f;
                break;
            case State.Firing:
                lineRenderer.startWidth = 0;
                lineRenderer.endWidth = 0;
                break;
            case State.Fire:
                lineRenderer.startWidth -= 0.1f * Time.deltaTime;
                lineRenderer.startColor = Color.Lerp(lineRenderer.startColor, Color.red, 1 - lineRenderer.startWidth);
                break;
            case State.Reloading:
                lineRenderer.startWidth = 0;
                break;



        }

    }
}
