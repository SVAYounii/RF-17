using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject Enemy;
    public List<Vector3> SpawnPointsFirst = new List<Vector3>();
    public List<Vector3> SpawnPointsSecond = new List<Vector3>();
    public List<Vector3> SpawnPointsThird = new List<Vector3>();

    public bool RoomOne = false;
    public bool RoomTwo = false;

    public int Wave;
    public bool IsInWave;
    public int EnemiesLeft;

    public float Timer = 60;
    public float CurrentTime;
    private float _timeForNextWave;

    public TextMeshProUGUI EnemyText;
    public TextMeshProUGUI CounterText;
    public TextMeshProUGUI WaveCounter;



    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = Timer;
        _timeForNextWave = Time.time + Timer;

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTime > 0 && !IsInWave)
        {
            CurrentTime -= Time.deltaTime;
        }

        if (EnemiesLeft == 0 && IsInWave)
        {
            IsInWave = false;
            _timeForNextWave = Time.time + Timer;
        }

        if (!IsInWave && Time.time > _timeForNextWave)
        {
            Spawn();
        }

        if (!IsInWave)
        {
            CounterText.enabled = true;
        }
        else
        {
            CounterText.enabled = false;

        }
        EnemiesLeft = transform.childCount;
        EnemyText.text = EnemiesLeft.ToString();
        CounterText.text = CurrentTime.ToString("F0");
    }

    void Spawn()
    {
        Wave++;
        WaveCounter.text = "Wave: " + Wave;
        IsInWave = true;
        CurrentTime = Timer;

        for (int i = 0; i < SpawnPointsFirst.Count; i++)
        {
            GameObject e = Instantiate(Enemy, SpawnPointsFirst[i], new Quaternion());
            e.transform.parent = transform;
        }

        if (RoomOne)
        {
            for (int i = 0; i < SpawnPointsSecond.Count; i++)
            {
                GameObject e = Instantiate(Enemy, SpawnPointsSecond[i], new Quaternion());
                e.transform.parent = transform;
            }
        }

        if (RoomTwo)
        {
            for (int i = 0; i < SpawnPointsThird.Count; i++)
            {
                GameObject e = Instantiate(Enemy, SpawnPointsThird[i], new Quaternion());
                e.transform.parent = transform;
            }
        }



    }



}
