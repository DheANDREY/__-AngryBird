using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird _shotBird;
    public TrailController TrailController;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _panelLose;
    public bool IsOver { get; private set; }
 
    private bool _isGameEnded = false;
    public BoxCollider2D TapCollider;

    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }
    public void ChangeBird()
    {
        TapCollider.enabled = false;
        if (_isGameEnded)
        {
            return;
        }
        Birds.RemoveAt(0);

        if (Birds.Count > 0) { 
            SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
        }
    }
    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }
    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);                
                break;
            }
        }
        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
        }

    }
    public void _win(bool win)
    {
        if (win)
        {
            _panelWin.gameObject.SetActive(true);
        }
        else
        {
            _panelLose.gameObject.SetActive(true);
        }
    }
        
}


