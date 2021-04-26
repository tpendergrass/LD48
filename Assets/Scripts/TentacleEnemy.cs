using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleEnemy : MonoBehaviour {
    public GameObject target;
    public EnemyState state;
    public float rotateSpeed;
    public float attackRadius;
    public float attackRate;
    private float lastAttack = 9999;
    public Animator anim;
    private Vector3 idleLookDirection;
    public GameObject TentacleAttackBox;
    public bool isHiding;
    public GameObject burrowEffect;

    void Start() {
        InvokeRepeating("pickRandomDirection", 0.0f, Random.Range(3, 6));
        anim.Update(Random.Range(0, 100)); //prevent animation syncing at spawn.
    }

    void pickRandomDirection() {
        Vector2 lookOffset = Random.insideUnitCircle;
        idleLookDirection = transform.position;
        idleLookDirection.x += lookOffset.x;
        idleLookDirection.y += lookOffset.y;
    }

    // Update is called once per frame
    void Update() {
        switch(state) {
            case EnemyState.Sleeping:
                anim.SetInteger("AnimState", -1);
                break;
            case EnemyState.Idle:
                anim.SetInteger("AnimState", 0);
                lookAt(idleLookDirection);
                //swing your tentacles back and forth
                break;
            case EnemyState.Chasing:
                anim.SetInteger("AnimState", 1);
                handleChasing();
                CheckForAttackDistance();
                break;
            case EnemyState.Attacking:
                anim.SetInteger("AnimState", 1);
                handleChasing();
                CheckForAttackDistance();
                Attack();
                break;
        }
    }

    void Attack() {
        if(lastAttack > attackRate) {
            TentacleAttackBox.SetActive(true);
            anim.SetInteger("AnimState", 2);
            lastAttack = 0;
        } else {
            lastAttack += Time.deltaTime;
        }

    }

    void CheckForAttackDistance() {
        if(Vector3.Distance(target.transform.position, transform.position) < attackRadius) {
            SetState(EnemyState.Attacking);
        } else {
            SetState(EnemyState.Chasing);
        }
    }

    public void DetectedFlare() {
        SetState(EnemyState.Sleeping);
        isHiding = true;
    }

    void DetectedTarget(GameObject newTarget) {
        target = newTarget;
        SetState(EnemyState.Chasing);
    }

    void LostTarget(GameObject newTarget) {
        SetState(EnemyState.Idle);
        target = null;
    }

    void SetState(EnemyState newState) {
        if(newState == EnemyState.Sleeping) {
            burrowEffect.SetActive(true);
        }

        if(state == EnemyState.Sleeping && newState == EnemyState.Chasing) {
            burrowEffect.SetActive(true);
        }

        if(!isHiding) {
            state = newState;
        }
    }

    void handleChasing() {
        lookAt(target.transform.position);
    }

    void lookAt(Vector3 position) {
        Vector3 lookPosition = position - transform.position;
        lookPosition.y = 0;
        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            Quaternion.LookRotation(lookPosition), 
            rotateSpeed * Time.deltaTime
        );
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (transform.forward * attackRadius) + transform.position);
    }
}
