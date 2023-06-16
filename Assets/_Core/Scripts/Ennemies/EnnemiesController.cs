using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnnemiesController : MonoBehaviour
{
    /*
     * effet de surprise quand il voit un joueur (le voit directement s'il a la lumière allumer)
     * infflige des degats au joueur
     * animation quand il prend un degat et quand il meurt
     */

    private GameManager manager;
    private Animator animator;
    private NavMeshAgent agent;
    private int increment;
    private bool detectPlayer;

    [Header("PNJ settings")]
    public float PNJSpeed = 2.5f;
    public Transform[] Destination;

    [Header("AI settings")] 
    public float NavigationSpeed = 3.5f;
    public float AttackDistance = 2;
    public float DetectionDistanceMin = 5;
    public float DetectionDistanceMax = 10;
    
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        foreach (GameObject _player in manager.Player)
        {
            float _distance;
            if (_player.GetComponent<PlayerController>().LightIsActive) _distance = DetectionDistanceMax;
            else _distance = DetectionDistanceMin;
            
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_player.transform.position.x, _player.transform.position.z)) <= _distance) AttackPlayer(_player);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_player.transform.position.x, _player.transform.position.z)) > _distance) MovementPNJ();
        }
    }

    void AttackPlayer(GameObject _player)
    {
        animator.SetBool("Walking", false);
        agent.speed = NavigationSpeed;
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(_player.transform.position.x, _player.transform.position.z)) >= AttackDistance)
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);
            agent.destination = _player.transform.position;
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            agent.destination = transform.position;
        }
    }

    void MovementPNJ()
    {
        agent.speed = PNJSpeed;
        animator.SetBool("Attack", false);
        animator.SetBool("Run", false);
        animator.SetBool("Walking", true);
        int _maxValue = Destination.Length;
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Destination[increment].position.x, Destination[increment].position.z)) <= 2)
        {
            increment ++;
            if (increment >= _maxValue) increment = 0;
            agent.destination = Destination[increment].position;
        }
        else agent.destination = Destination[increment].position;
    }
}
