using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPlayerDead
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
    [SerializeField] private Transform[] _axes = new Transform[12];
    private Vector3 _axesOfRotate = Vector3.zero;
    private float _angle = 0;
    // Start is called before the first frame update

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Ход: " + txt);
    }

    private void Awake()
    {
       rb = GetComponent<Rigidbody>();
        _axes = GetComponentsInChildren<Transform>();
    }

    private void OnMouseDown()
    {
        pos1 = Input.mousePosition;

    }

    private void OnMouseUp()
    {
        pos2 = Input.mousePosition;
        dir = pos2 - pos1;
        var x = dir.x;
        var z = dir.y;
        txt++;

        if (Mathf.Abs(x) > Mathf.Abs(z))
        {
            if (x > 0) FindAxesOfRotate(new Vector3(-100, 0, 0), Vector3.back);
            else FindAxesOfRotate(new Vector3(100, 0, 0), Vector3.back);
        }
        else if (Mathf.Abs(z) > Mathf.Abs(x))
         {
            if (z > 0) FindAxesOfRotate(new Vector3(0, 0, -100), Vector3.left);
            else FindAxesOfRotate(new Vector3(0, 0, 100), Vector3.left);
        }

    }

    private void FindAxesOfRotate(Vector3 temp, Vector3 dir)
    {
        _axesOfRotate = temp;
        if (temp.x < 0)
        {
            _angle = 90;
            foreach (var obj in _axes)
            {
                if (obj.position.y < 0.6)
                {
                    if (obj.position.x > _axesOfRotate.x)
                        _axesOfRotate = obj.position;
                }
            }
        }
        else if (temp.x > 0)
        {
            _angle = -90;
            foreach (var obj in _axes)
            {
                if (obj.position.y < 0.6)
                {
                    if (obj.position.x < _axesOfRotate.x)
                        _axesOfRotate = obj.position;
                }
            }
        }
        else if (temp.z < 0)
        {
            _angle = -90;
            foreach (var obj in _axes)
            {
                if (obj.position.y < 0.6)
                {
                    if (obj.position.z > _axesOfRotate.z)
                        _axesOfRotate = obj.position;
                }
            }
        }
        else if (temp.z > 0)
        {
            _angle = 90;
            foreach (var obj in _axes)
            {
                if (obj.position.y < 0.6)
                {
                    if (obj.position.z < _axesOfRotate.z)
                        _axesOfRotate = obj.position;
                }
            }
        }
        coroutine = Move(_axesOfRotate, _angle, dir);
        StartCoroutine(coroutine);
    }

    public void PlayerDead()
    {
        if (_enter)
        {
            GetComponent<MeshRenderer>().enabled = false;
            _explode.Play();
            Invoke("retryScene", 2.0f);
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
        _enter = false;
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = true;
        while (transform.position.y > -0.2)
        {
            transform.position = transform.position - new Vector3(0, 0.01f, 0);
            yield return null;
        }
        SceneManager.LoadScene(1);
    }

    private IEnumerator Move(Vector3 dist, float ang, Vector3 dir)
    {
        rb.isKinematic = true;
        for (int i = 0; i <= 9; i++)
        {
            transform.RotateAround(dist, dir, ang / 10);
            yield return new WaitForSeconds(0.03f);
        }
        foreach (var obj in _axes)
        {
            if (obj.position.y < 0.6)
            {
                if (obj.name == "9" || obj.name == "10" || obj.name == "11" || obj.name == "12") _enter = false;
                else _enter = true;
            }
        }
        rb.isKinematic = false;
    }


}
