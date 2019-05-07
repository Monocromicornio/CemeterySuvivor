using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorBehaviour : MonoBehaviour {

    [SerializeField]
    GameObject player;
    [SerializeField]
    Animator anim;

    [SerializeField]
    int lifeWarrior;
    [SerializeField]
    int playerDamage;
    [SerializeField]
    int pointsToPlayer;

    [SerializeField]
    GameObject[] itensToDrop;

    int dropChance;
    int randIntem;

    float Cooldown = 2;

    bool live = true;

    bool flag;

    NavMeshAgent navMesh;

	void Start () {
        player = GameObject.Find("Player");
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (live)
        {
            Cooldown -= Time.deltaTime;

            if (Vector3.Distance(transform.position, player.transform.position) >= 2f)
            {
                navMesh.destination = player.transform.position;
                anim.SetBool("Running", true);
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= 3f)
            {
                anim.SetBool("Running", false);
                if (Cooldown <= 0)
                {
                    anim.SetBool("Attack", true);
                    Cooldown = 2;
                }
                else
                {
                    anim.SetBool("Attack", false);
                }

            }
        }

        if(lifeWarrior <= 0)
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
        if (other.gameObject.tag == "PlayerArrow")
        {
            lifeWarrior -= playerDamage;            
        }
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
