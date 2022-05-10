using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class PlayerAgent : Agent
{
    public enum Team
    {
        BLUE = 0, RED
    }

    public Team team;

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
        GetComponent<Renderer>().material = materials[(int)team];
        InitPlayer();
    }

    public void InitPlayer()
    {
        transform.localPosition = (team == Team.BLUE) ? initPosBlue : initPosRed;
        transform.localRotation = (team == Team.BLUE) ? initRotBlue : initRotRed;
    }

}
