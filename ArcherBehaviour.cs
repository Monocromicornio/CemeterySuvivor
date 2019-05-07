using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ArcherBehaviour : MonoBehaviour {

    [SerializeField]
    GameObject player;
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject arrow;
    [SerializeField]
    Transform arrowPosition;
    [SerializeField]
    float CooldownTime;

    [SerializeField]
    int lifeArcher;
    [SerializeField]
    int playerDamage;
    [SerializeField]
    int pointsToPlayer;

    [SerializeField]
    GameObject[] itensToDrop;

    int dropChance;
    int randIntem;

    bool live = true;

    bool flag;

    float Cooldown;

    NavMeshAgent archerNavMesh;


    void Start () {
        player = GameObject.Find("Player");
        archerNavMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Cooldown = CooldownTime;
    }
	
	// Update is called once per frame
	void Update () {
        Cooldown -= Time.deltaTime;

        

        if (live)
        {
            transform.LookAt(player.transform.position);

            if (Vector3.Distance(transform.position, player.transform.position) >= 10f)
            {
                anim.SetBool("Running", true);
                archerNavMesh.destination = player.transform.position;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= 14)
            {
                anim.SetBool("Running", false);
                if (Cooldown <= 0)
                {
                    anim.SetBool("Shotting", true);
                    Instantiate(arrow, arrowPosition.position, arrowPosition.rotation);
                    Cooldown = CooldownTime;
                }
                else
                {
                    anim.SetBool("Shotting", false);
                }

            }
        }       

        if (lifeArcher <= 0)
        {            
            anim.SetBool("Death", true);
            live = false;
            StartCoroutine(destroy());
            if (!flag)
            {
                dropChance = Random.Range(0, 10);
                randIntem = Random.Range(0, itensToDrop.Length);
                if (dropChance >= 8)
                {
                    Instantiate(itensToDrop[randIntem], gameObject.transform.position, Quaternion.identity);
                }
                attScore();
                flag = true;
            }
            
        }
    }

    void attScore()
    {
        CanvasController.score += pointsToPlayer;
        CanvasController.kills += 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerArrow")
        {
            lifeArcher -= playerDamage;            
        }
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
