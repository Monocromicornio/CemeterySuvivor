using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    [SerializeField]
    int velArrow = 10;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0,0 -velArrow)  * Time.deltaTime);

        Destroy(this.gameObject, 3);
	}
}
