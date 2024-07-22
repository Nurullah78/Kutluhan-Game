using System;
using UnityEngine;

namespace WarScene
{
    public class SpawnEffect : MonoBehaviour
    {

//---------------Nesneler---------------

        [SerializeField]
        AnimationCurve fadeIn;
        [SerializeField]
        float spawnEffectTime = 2, pause = 1;

        [NonSerialized]
        public float timer;


        //---------------Metotlar---------------

        [SerializeField]
         ParticleSystem ps;
        Renderer _renderer => GetComponent<Renderer>();
        int shaderProperty => Shader.PropertyToID("_cutoff");


        void Start()
        {
            var main = ps.main;
            main.duration = spawnEffectTime;

        }

        void Update()
        {
            if (timer <= 0.2f)
            {
                ps.Play();
            }

            if (timer < spawnEffectTime + pause)
            {
                timer += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }


            _renderer.material.SetFloat(shaderProperty, fadeIn.Evaluate(Mathf.InverseLerp(0, spawnEffectTime, timer)));
        }
    }
}
