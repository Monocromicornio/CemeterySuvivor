using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float speed;    

    [SerializeField]
    int playerLife = 100;
    [SerializeField]
    Slider lifeBar;
    [SerializeField]
    int damageFromArcher;
    [SerializeField]
    int damageFromWarrior;
    [SerializeField]
    int jumpForce;
    [SerializeField]
    int heartCure;

    Rigidbody rb;
       

    [SerializeField]
    GameObject GameOver;

    Camera cam;

	void Start () {
        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {        

    if(CanvasController.gameStarted == true && playerLife > 0)
        {

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.Space) && gameObject.transform.position.y < 1.62f)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            RotateView();

        }

        lifeBar.value = playerLife;

        if(playerLife <= 0)
        {
            GameOver.SetActive(true);
            StartCoroutine(backToMenu());
        }
      
    }

    void RotateView()
    {
        transform.Rotate(Vector3.up * 200 * Input.GetAxis("Mouse X") * Time.deltaTime);

        cam.transform.Rotate(Vector3.left * 200 * Input.GetAxis("Mouse Y") * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.tag == "Arrow")
        {
            playerLife -= damageFromArcher;         
        }
        if (hit.gameObject.tag == "Sword")
        {
            playerLife -= damageFromWarrior;
        }
        if (hit.gameObject.tag == "Heart")
        {
            playerLife += heartCure;
            Destroy(hit.gameObject);
        }
    }
        

    

    IEnumerator backToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }

}
