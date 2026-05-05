using System.Collections;
using UnityEngine;

public class Mongoose : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {

    }


    public void Move(Vector2 direction)
    {
        FaceCorrectDirection(direction);

        Vector2 movementAmount = GameParameters.MongooseMoveSpeed * direction * Time.deltaTime;
        spriteRenderer.transform.Translate(movementAmount.x, movementAmount.y, 0);

        spriteRenderer.transform.position = SpriteTools.ConstrainToScreen(spriteRenderer);
    }


    private void FaceCorrectDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public Vector3 GetPosition()
    {
        return spriteRenderer.transform.position;
    }

}


