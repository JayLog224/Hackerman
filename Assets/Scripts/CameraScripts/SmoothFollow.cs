using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{

    private float dampX = 0.2f;
    private float dampY = 0.2f;
    public float velocityX = 0f;
    public float velocityY = 0f;
    public Transform target;
    public GameObject background;

    private float camXLimit, camYLimit;

    //public float offset;

    public GameObject cursor;

    // Use this for initialization
    void Start()
    {
        Sprite bg = background.GetComponent<SpriteRenderer>().sprite;
        Camera cam = gameObject.GetComponent<Camera>();

        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        camXLimit = bg.bounds.size.x/2 - camWidth / 2;
        camYLimit = bg.bounds.size.y/2 - camHeight / 2;
        Debug.Log("x limit:" + camXLimit);
        Debug.Log("y limit:" + camYLimit);
        Debug.Log("cam rect x: " + camWidth + " cam rect y: " + camHeight);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mouseDir = (cursor.transform.position - target.position).normalized * 0.8f; //how to calculate vec3 direction and add in direction

        Vector2 targetOffset = target.position + target.up * 0.5f ; // * offset si tuviera uno

        float posX = Mathf.SmoothDamp(transform.position.x, targetOffset.x + mouseDir.x, ref velocityX, dampX);
        float posY = Mathf.SmoothDamp(transform.position.y, targetOffset.y + mouseDir.y, ref velocityY, dampY);

        //Debug.Log("cam x: " + posX + " Cam y: " + posY);
        

         //chequear por el background
        
        transform.position = new Vector3(Mathf.Clamp(posX, -camXLimit, camXLimit),
                                         Mathf.Clamp(posY, -camYLimit, camYLimit),
                                         transform.position.z);
    }


    // DEBERIA PROBAR HACER UN BOUNDS QUE CONTENGA TANTO AL PLAYER COMO A LA MIRA, PERO QUE SEA FIJO, ES DECIR QUE LOS MANTENGA JUSTAMENTE DENTRO DE ESOS LIMITES Y APUNTE LA CAMARA AL MEDIO
}