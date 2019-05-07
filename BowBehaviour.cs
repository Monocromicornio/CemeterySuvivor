using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowBehaviour : MonoBehaviour {

    [SerializeField]
    Animator anim;

    [SerializeField]
    Transform arrowPostition;
    [SerializeField]
    GameObject arrow;       

    [SerializeField]
    float arrowFireRate; //CoolDown
        
    [SerializeField]
    float force;
    [SerializeField]
    float maxTimeToPrepare;
    [SerializeField]
    float timeToSpawNewArrow;
    

    float currentTimeToNewArrow;

    GameObject currentArrow;

    float currentForce;

    float fireRate;

    void Start () {
        anim = GetComponent<Animator>();
        
        spawArrows();
        arrowFireRate = fireRate;
	}
	
	// Update is called once per frame
	void Update () {
        if(currentArrow != null)
        {
            if (currentArrow.transform.parent == null)
            {
                currentTimeToNewArrow += Time.deltaTime;
                if (currentTimeToNewArrow > timeToSpawNewArrow)
                {
                    currentTimeToNewArrow = 0;
                    spawArrows();
                }
            }
        }
       

        fireRate += Time.deltaTime;
                

        if (Input.GetMouseButtonDown(1) && fireRate > arrowFireRate && currentArrow != null)
        {
            fireRate = 0;            
            anim.SetBool("Preparing", true);
            anim.SetBool("Attack", false);

            if(currentForce > maxTimeToPrepare)
            {
                currentForce += Time.deltaTime;
            }

        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("Attack", true);
            anim.SetBool("Preparing", false);            
            Lauch();
            currentForce = 0;
        }
    }

    void spawArrows()
    {
        currentArrow = Instantiate(arrow, arrowPostition.position, arrowPostition.rotation) as GameObject;
        currentArrow.transform.SetParent(arrowPostition);
    }

    void Lauch()
    {
        currentArrow.transform.parent = null;
        currentArrow.GetComponent<Rigidbody>().useGravity = true;
        currentArrow.GetComponent<Rigidbody>().isKinematic = false;
        currentArrow.GetComponent<SphereCollider>().enabled = true;

        Vector3 rotation = currentArrow.transform.localEulerAngles;
        rotation.x = -90;
        currentArrow.transform.localEulerAngles = rotation;

        currentArrow.GetComponent<Rigidbody>().AddRelativeForce(Vector3.down * force , ForceMode.Impulse);        
        
    }
}
