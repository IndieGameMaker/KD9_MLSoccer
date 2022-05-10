using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class BallCtrl : MonoBehaviour
{
    public Agent[] players;

    private Rigidbody rb;

    [SerializeField]
    private int blueScore;
    [SerializeField]
    private int redScore;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void InitBall()
    {
        transform.localPosition = new Vector3(0, 2.0f, 0);
        rb.velocity = rb.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BLUE_GOAL"))
        {
            // RED +1, BLUE -1
            players[0].AddReward(-1.0f);
            players[1].AddReward(+1.0f);

            players[0].EndEpisode();
            players[1].EndEpisode();

            InitBall();

            ++redScore;
        }

        if (coll.collider.CompareTag("RED_GOAL"))
        {
            // RED -1, BLUE +1
            players[0].AddReward(+1.0f);
            players[1].AddReward(-1.0f);

            players[0].EndEpisode();
            players[1].EndEpisode();

            InitBall();

            ++blueScore;
        }
    }
}
