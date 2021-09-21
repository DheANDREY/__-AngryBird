using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    [SerializeField]
    private float _explodeRadius = 1.5f;

    [SerializeField]
    private float _explodePower = 2.5f;
    public AudioSource boom;

    private bool isExplode = false;

    public void Boost()
    {
        if (State == BirdState.HitSomething &&
            !isExplode)
        {
            Vector2 explodePos = transform.position;
            Collider2D[] collider = Physics2D.OverlapCircleAll(explodePos, _explodeRadius);

            foreach (Collider2D hit in collider)
            {
                Rigidbody2D rigidbody2D = hit.GetComponent<Rigidbody2D>();
                Vector2 dir = hit.gameObject.transform.position - transform.position;

                if (rigidbody2D != null)
                {
                    rigidbody2D.AddForce(dir * _explodePower, ForceMode2D.Impulse);
                }
            }

            isExplode = true;
            boom.Play();
        }
    }
    public override void OnTap()
    {
        Boost();
    }
}
