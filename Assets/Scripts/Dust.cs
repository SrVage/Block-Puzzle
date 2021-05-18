using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    [SerializeField] ParticleSystem _dust = null;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        _dust.Play();
    }
}
