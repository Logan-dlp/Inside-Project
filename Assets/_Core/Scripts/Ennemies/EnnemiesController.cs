using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnnemiesController : MonoBehaviour
{
    /*
     * (2 modes)
     * Tableau de point que l'ennemie vas suivre (Quand il ne voit pas le joueur) *
     * effet de surprise quand il voit un joueur (le voit directement s'il a la lumière allumer)
     * il suit le joueur jusqu'a un certain rayon
     * infflige des degats au joueur
     * option : si le joueur atteint un certain rayon l'ennemis arrete de le suivre
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
    public float NavigationSpeed = 5;
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
            else MovementPNJ();
        }
    }

    void AttackPlayer(GameObject _player)
    {
        agent.speed = NavigationSpeed;
        agent.destination = _player.transform.position;
    }

    void MovementPNJ()
    {
        agent.speed = PNJSpeed;
        animator.SetBool("Walking", true);
        int _maxValue = Destination.Length;
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Destination[increment].position.x, Destination[increment].position.z)) <= 2)
        {
            increment ++;
            if (increment >= _maxValue) increment = 0;
            agent.destination = Destination[increment].position;
        }
        else if (increment == 0) agent.destination = Destination[increment].position;
    }
}
