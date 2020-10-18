using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    private Vector2 targetPosition;
    private float attackTime;

    public float timeBetweenSummons;
    public float stopDistance;
    private float summonTime;
    public float attackSpeed;
    public Enemy enemyToSummon;
    private Animator anim;

    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > 12f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if(Time.time>= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("summon");
                    
                }
            }

            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time > attackTime)
                {              
                    attackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    public void Summon()
    {
        if(player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
        
    }
    IEnumerator Attack()
    {
        player.GetComponent<PlayerController>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }
    }
}
