using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerAgent : Agent
{
    public enum Team
    {
        BLUE = 0, RED
    }

    public Team team;

    private BehaviorParameters bps;
    private Rigidbody rb;

    // 이동속도, 킥파워
    public float moveSpeed = 1.0f;
    public float kickForce = 800.0f;

    //플레이어의 초기 위치
    public Vector3 initPosBlue = new Vector3(-5.5f, 0.5f, 0.0f);
    public Vector3 initPosRed = new Vector3(+5.5f, 0.5f, 0.0f);

    //플레이어의 초기 회전값
    public Quaternion initRotBlue = Quaternion.Euler(Vector3.up * 90);
    public Quaternion initRotRed = Quaternion.Euler(Vector3.up * -90);

    //플레이어의 색상 변경할 머티리얼
    public Material[] materials;

    public override void Initialize()
    {
        bps = GetComponent<BehaviorParameters>();
        bps.TeamId = (int)team;

        rb = GetComponent<Rigidbody>();
        rb.mass = 10.0f;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        rb.constraints = RigidbodyConstraints.FreezePositionY
                        | RigidbodyConstraints.FreezeRotationX
                        | RigidbodyConstraints.FreezeRotationZ;

        GetComponent<Renderer>().material = materials[(int)team];
        MaxStep = 10000;
    }

    public override void OnEpisodeBegin()
    {
        // 플레이어의 초기화
        InitPlayer();

        // 물리엔진 초기화
        rb.velocity = rb.angularVelocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(ActionBuffers actions)
    {

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // 이산값으로 파라메터 정의
        var actions = actionsOut.DiscreteActions;
        // 파라메터 초기화
        actions.Clear();

        // Branch[0]
        // Branch Size = 3
        // 정지, 전진, 후진 (0, 1, 2) 이동 => 키보드 Non, W, S
        if (Input.GetKey(KeyCode.W)) actions[0] = 1;
        if (Input.GetKey(KeyCode.S)) actions[0] = 2;

        // Branch[1]
        // 정지, 좌, 우 (0, 1, 2) 이동 => 키보드 Non, Q, E
        if (Input.GetKey(KeyCode.Q)) actions[1] = 1;
        if (Input.GetKey(KeyCode.E)) actions[1] = 2;

        // Branch[2]
        // 정지, 좌, 우 (0, 1, 2) 회전 => 키보드 Non, A, D
        if (Input.GetKey(KeyCode.A)) actions[2] = 1;
        if (Input.GetKey(KeyCode.D)) actions[2] = 2;

    }

    public void InitPlayer()
    {
        transform.localPosition = (team == Team.BLUE) ? initPosBlue : initPosRed;
        transform.localRotation = (team == Team.BLUE) ? initRotBlue : initRotRed;
    }

}
