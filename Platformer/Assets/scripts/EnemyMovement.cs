using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType { Static, SimpleMover, Smart}

[ExecuteInEditMode]
public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public MonsterType monsterType = MonsterType.Static;
    public Transform player;
    public float distance = 5.0f;

    public Vector2 position;


    private Rigidbody2D rigidbody;

    public Vector2 min = new();
    public Vector2 max = new(5, 5);


    private Vector2 dir = new Vector3(1.0f, 1.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (monsterType == MonsterType.Static)
            dir = new();
        if (monsterType == MonsterType.SimpleMover)
        {
            dir = max - min;
            rigidbody.position = (min + max) / 2.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (monsterType == MonsterType.Static)
            return;

        if(monsterType == MonsterType.SimpleMover)
        {
            if ((rigidbody.position - min).magnitude < 0.1f || (rigidbody.position - max).magnitude < 0.1f)
                dir *= -1.0f;
        }

        if (monsterType == MonsterType.Smart)
            dir = getSmartDir();

        move(dir * speed);
    }

    Vector2 getSmartDir()
    {
        Vector2 res = new();
        Vector2 playerPos = new Vector2(player.position.x, player.position.y);

        if ((playerPos - rigidbody.position).magnitude <= distance)
            res = playerPos - rigidbody.position;

        return res;
    }

    void move(Vector2 v)
    {
        rigidbody.velocity = v;
    }
}
