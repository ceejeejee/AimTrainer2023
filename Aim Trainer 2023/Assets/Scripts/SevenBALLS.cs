using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SevenBALLS : MonoBehaviour
{
    private int score;
    private List<int> sessionScores = new List<int>();
    private int totalShots;
    private List<float> sessionAccuracies = new List<float>();
    private bool hasStarted = false;
    private bool isPaused = false;
    private float targetTime;
    private RaycastHit hit;
    public GameObject ballPrefab;
    public GameObject mainCamera;
    public GameObject controlManager;
    public GameObject pauseMenu;
    public float setTargetTime = 60.0f;

    // Player Cmaera Movement
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Awake()
    {
        totalShots = 0;
        targetTime = setTargetTime;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Start()
    {
        score = 0;
    }

    void Update()
    {
        if(!isPaused)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            mainCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        if(hasStarted && !isPaused)
        {
            targetTime -= Time.deltaTime;
            if(targetTime <= 0.0f)
            {
                roundEnd(true);
            }
        }
        if(controlManager.GetComponent<Controls>().isPressingShoot() && !isPaused)
        {   
            if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 10))
            {
                if(hit.transform.gameObject.tag == "start"  && !hasStarted)
                {
                    hasStarted = true;
                    for(int i = 0; i < 7; i++)
                    {
                        Instantiate(ballPrefab, getCollisionFreePos(), Quaternion.identity);
                    }
                }
                else if(hit.transform.gameObject.tag == "ball" && hasStarted)
                {
                    // Debug.Log("Hit " + hit.transform.gameObject.name);
                    GameObject hitObj = hit.transform.gameObject;
                    Destroy(hitObj);
                    Instantiate(ballPrefab, getCollisionFreePos(), Quaternion.identity);
                    score++;
                    totalShots++;
                }
                else if(hit.transform.gameObject.tag == "start"  && hasStarted)
                {
                    roundEnd(false);
                } 
                else if(hasStarted)
                {
                    totalShots++;
                }
            }
        }
        if (controlManager.GetComponent<Controls>().isPressingQuit())
        {
            onQuit();
        }
        if (controlManager.GetComponent<Controls>().isPressingRestart())
        {
            roundEnd(false);
        }
        if (controlManager.GetComponent<Controls>().isPressingPause())
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Cursor.visible = isPaused;
            if(isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private Vector3 getCollisionFreePos()
    {
        Vector3 newPos = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.2f, 1.5f), 0);
        // check to see if where the ball would spawn, if there's any balls present. pick a new spot if that's the case
        while(Physics.OverlapSphere(newPos, ballPrefab.GetComponent<SphereCollider>().radius * ballPrefab.transform.localScale.x * (float)1.05).Length > 0)
        {
            Debug.Log("found overlapping balls");
            newPos = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(0.2f, 1.5f), 0);
            Debug.Log("reset position vector");
        }
        return newPos;
    }

    public void roundEnd(bool timeOut = false)
    {
        hasStarted = false;
        targetTime = setTargetTime;
        if(timeOut)
        {
            sessionScores.Add(score);
            sessionAccuracies.Add((float)score/totalShots);
        }
        score = 0;
        totalShots = 0;
        GameObject[] allBalls = GameObject.FindGameObjectsWithTag("ball");
        foreach(GameObject ball in allBalls)
        {
            Destroy(ball);
        }
    }

    public int getScore()
    {
        return score;
    }

    public List<int> getScores()
    {
        return sessionScores;
    }

    public List<float> getAccuracies()
    {
        return sessionAccuracies;
    }

    public float getTimeLeft()
    {
        return targetTime;
    }

    public void onQuit()
    {
        Application.Quit();
    }

}
