using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveAmount;

    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveAmount = moveInput.normalized * speed;
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthUI(health);

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeWeapon(WeaponController weaponToEquip)
    {
        Debug.Log("Equipped");
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

      void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i<currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);
    }




}
