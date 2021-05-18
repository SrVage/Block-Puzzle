using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour, IPlayerDead
{
    [SerializeField] private Vector3 pos1 = Vector3.zero;
    [SerializeField] private Vector3 pos2 = Vector3.zero;
    [SerializeField] private Vector3 dir = Vector3.zero;
    [SerializeField] private GameObject v1 = null;
    [SerializeField] private GameObject v2 = null;
    [SerializeField] Camera cam = null;
    [SerializeField] ParticleSystem _explode = null;
   private Rigidbody rb = null;
    public bool _enter = false;
    [SerializeField] private Vector3 lay = Vector3.zero;
    private int txt = 0;
    private IEnumerator coroutine = null;
    private Vector3 vec;
    // Start is called before the first frame update

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Ход: " + txt);
    }

    private void Awake()
    {
       rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        //RaycastHit hit;
        //var ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit))
        //pos1 = hit.point;
        pos1 = Input.mousePosition;

    }

    private void OnMouseUp()
    {
        //RaycastHit hit;
        //var ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit))
        //    pos2 = hit.point;
        pos2 = Input.mousePosition;
        dir = pos2 - pos1;
        var x = dir.x;
        var z = dir.y;
        txt++;

        if (Mathf.Abs(x) > Mathf.Abs(z))
        {
            if (x > 0)
            {
                if (lay.z!=0)
                {
                    coroutine = Right(new Vector3(1.0f,0,0), 90, Vector3.back, transform.position);
                    StartCoroutine(coroutine);
                    //transform.RotateAround(transform.position, Vector3.back, 90);
                    //transform.Translate(1f, 0, 0, Space.World);
                }
                else
                {
                    coroutine = Right(new Vector3(1.5f, 0, 0), 90, Vector3.back, transform.position);
                    StartCoroutine(coroutine);
                    //transform.RotateAround(transform.position, Vector3.back, 90);
                    //transform.Translate(1.5f, 0, 0, Space.World);
                }
            }
            else if (x < 0)
            {
                if (lay.z != 0)
                {
                    coroutine = Right(new Vector3(-1.0f, 0, 0), -90, Vector3.back, transform.position);
                    StartCoroutine(coroutine);
                    //transform.RotateAround(transform.position, Vector3.back, -90);
                    //transform.Translate(-1f, 0, 0, Space.World);
                }
                else
                {
                   coroutine = Right(new Vector3(-1.5f, 0, 0), -90, Vector3.back, transform.position);
                    StartCoroutine(coroutine);
                    //transform.RotateAround(transform.position, Vector3.back, -90);
                    //transform.Translate(-1.5f, 0, 0, Space.World);
                }
            }
        }
       else if (Mathf.Abs(z) > Mathf.Abs(x))
        {
            if (z > 0)
            {
                if (lay.x!=0)
                {
                    coroutine = Right(new Vector3(0, 0, 1.0f), -90, Vector3.left, transform.position);
                    StartCoroutine(coroutine);
                    //transform.RotateAround(transform.position, Vector3.left, 90);
                    //transform.Translate(0, 0, 1f, Space.World);
                }
                else
                {
                    coroutine = Right(new Vector3(0, 0, 1.5f), -90, Vector3.left, transform.position);
                    StartCoroutine(coroutine);
                    //transform.RotateAround(transform.position, Vector3.left, 90);
                    //transform.Translate(0, 0, 1.5f, Space.World);
                }
            }
            else if (z < 0)
            {
                if (lay.x != 0)
                {
                    coroutine = Right(new Vector3(0, 0, -1.0f), 90, Vector3.left, transform.position);
                    StartCoroutine(coroutine);
                    //transform.RotateAround(transform.position, Vector3.left, -90);
                    //transform.Translate(0, 0, -1f, Space.World);
                }
                else
                {
                    coroutine = Right(new Vector3(0, 0, -1.5f), 90, Vector3.left, transform.position);
                    StartCoroutine(coroutine);
                    //transform.RotateAround(transform.position, Vector3.left, -90);
                    //transform.Translate(0, 0, -1.5f, Space.World);
                }
            }
        }

    }

    public void PlayerDead()
    {
        if (_enter)
        {
            GetComponent<MeshRenderer>().enabled = false;
            _explode.Play();
        }
    }

    public void PlayerDead2()
    {
            //GetComponent<MeshRenderer>().enabled = false;
            //_explode.Play();
        Invoke("retryScene", 2.0f);
    }

    private void retryScene()
    {
SceneManager.LoadScene(2);
    }

    public void PlayerFinish()
    {
        if (_enter)
        StartCoroutine("PlayerMoveDown");
    }

     private IEnumerator PlayerMoveDown()
    {
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = true;
        while (transform.position.y > -0.2)
        {
            transform.position = transform.position - new Vector3(0, 0.01f, 0);
            yield return null;
        }
        SceneManager.LoadScene(1);
    }


    private void Update()
    {
        
        if (lay.y == 2 || lay.y == -2)
        {
            _enter = true;
           // transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        }
        else
        {
            _enter = false;
            //transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }

    }

    private IEnumerator Right(Vector3 dist, float ang, Vector3 dir, Vector3 posit)
    {
        rb.isKinematic = true;
        for (int i = 0; i <= 9; i++)
        {
            transform.RotateAround(transform.position, dir, ang / 10);
            transform.Translate(dist / 10, Space.World);
            //transform.position += dist;
            yield return new WaitForSeconds(0.03f);
        }
        transform.position = new Vector3(Convert.ToSingle(Math.Round(transform.position.x, 1)), Convert.ToSingle(Math.Round(transform.position.y, 1)), Convert.ToSingle(Math.Round(transform.position.z, 1)));
        transform.localEulerAngles = new Vector3(Mathf.Round(transform.localEulerAngles.x), Mathf.Round(transform.localEulerAngles.y), Mathf.Round(transform.localEulerAngles.z));
        lay = (v2.transform.position - v1.transform.position);
        rb.isKinematic = false;
        yield return new WaitForSeconds(0.03f);
        if (lay.x == 2 || lay.x == -2) lay = new Vector3(lay.x, 0, 0);
        if (lay.y == 2 || lay.y == -2) lay = new Vector3(0, lay.y, 0);
        if (lay.z == 2 || lay.z == -2) lay = new Vector3(0, 0, lay.z);


    }


}
