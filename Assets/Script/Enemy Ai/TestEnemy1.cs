using Unity.VisualScripting;
using UnityEngine;

public class TestEnemy1 : EnemyMoveBehavior
{
    Material originalMaterial;
    public Material highlightMaterial;
    public bool isHighlighted;
    bool onCooldown;
    float cooldownTimer;
    LevelManager manager;

    
    /*
    private void OnEnable()
    {
        originalMaterial = GetComponent<MeshRenderer>().material;
    }

    private void FixedUpdate()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            onCooldown = true;
        }
        else if (cooldownTimer <= 0) 
        {
            onCooldown = false; 
            weightedBehavior = false;

            if (Random.Range(1, 100) > 95 && Random.Range(1,5) > 3)
            {
                isHighlighted = true;
                gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;

                Debug.Log("Highlighted");
            }
        }


    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !onCooldown && isHighlighted)
        {
            Debug.LogWarning("Touching Enemy!");
            weightedBehavior = true;
            isHighlighted = false;
            cooldownTimer += 200;
        }
    }

    private void OnMouseEnter()
    {
        if (isHighlighted)
        {
            gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (isHighlighted)
        {
            gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = originalMaterial;

        }
    }*/
}
