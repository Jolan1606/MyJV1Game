using UnityEngine;
using System.Collections;
public class Raycast : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Camera cam;
    public ParticleSystem play;
    public ParticleSystem quit;
    void Start()
    {
        cam =Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 10f;
        mousepos =cam.ScreenToWorldPoint(mousepos);
        Debug.DrawLine(transform.position,mousepos-transform.position,Color.blue);
       Ray ray =cam.ScreenPointToRay(mousepos);
        RaycastHit hit;
        hit = GetComponent<RaycastHit>();
        if (Physics.Raycast(ray, out hit,100))
        {
            Debug.Log(hit.transform.name);



        }
    }
}
