using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeepBeingEnemy : MonoBehaviour {
    public GameObject target;
    public EnemyState state;
    public float rotateSpeed;
    public float attackRadius;
    public float attackRate;
    private float lastAttack = 9999;
    public Animator anim;
    private Vector3 idleLookDirection;
    public GameObject SlapperAttackBox;
    public NavMeshAgent agent;
    public SoundManager sound;

    void Start() {
        InvokeRepeating("pickRandomDirection", 0.0f, Random.Range(3, 6));
        anim.Update(Random.Range(0, 100)); //prevent animation syncing at spawn.
    }

    void pickRandomDirection() {
        Vector2 lookOffset = Random.insideUnitCircle * 10;
        idleLookDirection = transform.position;
        idleLookDirection.x += lookOffset.x;
        idleLookDirection.y += lookOffset.y;
        if(state == EnemyState.Idle) {
            agent.SetDestination(idleLookDirection);
        }
    }

    // Update is called once per frame
    void Update() {
        switch(state) {
            case EnemyState.Sleeping:
                anim.SetInteger("AnimState", -1);
                break;
            case EnemyState.Idle:
                if(agent.velocity.magnitude > 0.5) {
                    anim.SetInteger("AnimState", 1);
                } else {
                    anim.SetInteger("AnimState", 0);
                }
                break;
            case EnemyState.Chasing:
                anim.SetInteger("AnimState", 1);
                handleChasing();
                CheckForAttackDistance();
                break;
            case EnemyState.Attacking:
                anim.SetInteger("AnimState", 0);
                handleChasing();
                lookAt(target.transform.position);
                CheckForAttackDistance();
                Attack();
                break;
        }
    }

    void Attack() {
        if(lastAttack > attackRate) {
            SlapperAttackBox.SetActive(true);
            anim.SetInteger("AnimState", 2);
            lastAttack = 0;
            sound.Play(2);
        } else {
            lastAttack += Time.deltaTime;
        }

    }

    void CheckForAttackDistance() {
        if(Vector3.Distance(target.transform.position, transform.position) < attackRadius) {
            if(state != EnemyState.Attacking) {
                SetState(EnemyState.Attacking);
            }
        } else {
            if(state != EnemyState.Chasing) {
                SetState(EnemyState.Chasing);
            }
        }
    }

    void DetectedTarget(GameObject newTarget) {
        Debug.Log("New Target!");
        target = newTarget;
        SetState(EnemyState.Chasing);
    }

    void LostTarget(GameObject lostTarget) {
        if(lostTarget != target) {
            return;
        }
        SetState(EnemyState.Idle);
        target = null;
    }

    void SetState(EnemyState newState) {
        if(newState == EnemyState.Chasing) {
            sound.Play(0);
        }

        if(newState == EnemyState.Attacking) {
            sound.Play(1);
        }

        state = newState;
    }

    void handleChasing() {
        agent.SetDestination(target.transform.position);
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
