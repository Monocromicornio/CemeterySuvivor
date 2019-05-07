using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawController : MonoBehaviour {

    [SerializeField]
    GameObject[] spawPoints;
    [SerializeField]
    GameObject[] enemys;
    [SerializeField]
    int minTimetoRespaw;
    [SerializeField]
    int maxTimetoRespaw;

    int timeToRespaw;

    bool inGame = true;

    int randomPoint;
    int randomEnemy;
    

    void Start () {
        StartCoroutine("spawTime");
    }
	
	// Update is called once per frame
	void Update () {
        timeToRespaw = Random.Range(minTimetoRespaw, maxTimetoRespaw);
       
	}

    IEnumerator spawTime()
    {
        yield return new WaitForSeconds(5);

        while (inGame)
        {
            randomPoint = Random.Range(0, spawPoints.Length);
            randomEnemy = Random.Range(0, enemys.Length);

            Vector3 spawPosition = spawPoints[randomPoint].transform.position;

            Instantiate(enemys[randomEnemy], spawPosition, Quaternion.identity);

            yield return new WaitForSeconds(timeToRespaw);
        }
       

    }
}
