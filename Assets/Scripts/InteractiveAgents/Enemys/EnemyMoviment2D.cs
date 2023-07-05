using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMoviment2D : ObstacleMoviment
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!enemy.ignoreWall && collision.CompareTag(GameSettings.Instance.wallTag) || (collision.CompareTag(GameSettings.Instance.enemyTag) && !enemy.ignoreEnemys)))
            FlipMe(faceDirection);
    }

    private void FlipMe(Vector3 direction)
    {
        if(direction.x != 0)
            faceDirection.x *= -1;
        if (direction.y != 0)
            faceDirection.y *= -1;
        if (direction.z != 0)
            faceDirection.z *= -1;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && enemy.canFlip)
        {
            if (faceDirection.x != 0)
                spriteRenderer.flipX = !spriteRenderer.flipX;

            if (faceDirection.y != 0)
                spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }
}
