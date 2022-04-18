using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public BoxCollider2D topCol;
    public BoxCollider2D rightCol;
    public BoxCollider2D bottomCol;
    public BoxCollider2D leftCol;

    Vector3 currentPosition;
    Vector3 targetPosition;

    


    // Start is called before the first frame update
    void Start()
    {
        currentPosition = this.gameObject.transform.position;
        targetPosition = currentPosition;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collider2D innerCollider = collision.otherCollider;
            float positionX = this.gameObject.transform.position.x;
            float positionY = this.gameObject.transform.position.y;
            float targetPositionX = positionX;
            float targetPositionY = positionY;
            if (innerCollider == topCol)
            {
                targetPositionY = positionY - 1;
            }else if (innerCollider == bottomCol)
            {
                targetPositionY = positionY + 1;
            }
            else if (innerCollider == rightCol)
            {
                targetPositionX = positionX - 1;
            }
            else if (innerCollider == leftCol)
            {
                targetPositionX = positionX + 1;
            }



            //Checks obstacles in the way
            targetPosition = new Vector3(targetPositionX, targetPositionY, 0);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(targetPosition, 0.1f, LayerMask.GetMask("PhysicalObject"));
            if (hitColliders.Length <= 0)
            {
                this.transform.position = targetPosition;
            }
            //StartCoroutine(LerpPosition(targetPosition, 2));
           
        }

    }

    IEnumerator LerpPosition (Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = this.gameObject.transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Slerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }


}
