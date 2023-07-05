﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

public abstract class ObstacleMoviment : MonoBehaviour {

    [SerializeField]
    public bool canMove;
    //[SerializeField]
    //private DifficultyList enemysDifficulty;
    [SerializeField]
    protected FloatReference movementSpeed;
    public enum Directions { None, Up, Right, Down, Left, Forward, Backward, Free }
    [SerializeField]
    protected Directions moveDirection;
    protected Vector3 faceDirection;
    [SerializeField]
    protected Vector3 freeDirection;
    [SerializeField]
    protected AnimationCurve curveMoviment;

    #region Variable Private
    private Directions _moveDirection;
    private Vector3 _freeDirection;
    protected Vector3 enemyMovment;
    protected GamePlayMaster gamePlay;
    protected EnemyMaster enemyMaster;
    protected Enemy enemy;
    #endregion

    private float EnemySpeedy { get { return (enemy.enemysDifficulty.GetDifficult(GamePlayMaster.Instance.HightScorePlayers()).multplySpeedy > 0) ? movementSpeed.Value * enemy.enemysDifficulty.GetDifficult(GamePlayMaster.Instance.HightScorePlayers()).multplySpeedy : movementSpeed.Value; } }
    public Directions MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
    public Vector3 FreeDirection { get { return freeDirection; } set { freeDirection = value; } }
    public FloatReference MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public AnimationCurve CurveMoviment { get { return curveMoviment; } set { curveMoviment = value; } }

    private void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventResetEnemys += ResetMovement;
    }

    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        enemyMaster = GetComponent<EnemyMaster>();
        enemy = enemyMaster.enemy;
        _moveDirection = moveDirection;
        _freeDirection = freeDirection;
        SetDirection(moveDirection);
    }

    private void Update()
    {
        MoveEnemy();
    }

    protected virtual void MoveEnemy()
    {
        //Debug.Log("Canmove: "+ canMove + " Move dir: " + moveDirection + " Gameover?: " + gameManager.isGameOver + " Pause: " + gameManager.isPaused + " Destroy: " + enemyMaster.IsDestroyed);
        if (!canMove || !GamePlayMaster.Instance.ShouldBePlayingGame || moveDirection == Directions.None || enemyMaster.IsDestroyed)
            return;
        enemyMovment = faceDirection * EnemySpeedy * Time.deltaTime;
        transform.position += enemyMovment;
        if (curveMoviment.length > 1)
            transform.position = MoveInCurveAnimation(moveDirection);
    }

    private Vector3 MoveInCurveAnimation(Directions moveDirection)
    {
        float st = enemyMaster.EnemyStartPosition.y - 2;
        float st2 = enemyMaster.EnemyStartPosition.y + 2;
        float posy = Mathf.Lerp(st, st2, curveMoviment.Evaluate(Time.time));//enemyMaster.EnemyStartPosition.y + curveMovment.Evaluate(enemySpeedy / Time.time);
        //float posy = Mathf.Clamp(curveMovment.Evaluate(Time.time), enemyMaster.EnemyStartPosition.y - 3, enemyMaster.EnemyStartPosition.y + 3);
        return new Vector3(transform.position.x, posy, transform.position.y);
    }

    private void SetDirection(Directions dir)
    {
        switch (dir)
        {
            case Directions.Up:
                faceDirection = Vector3.up;
                return;
            case Directions.Right:
                faceDirection = Vector3.right;
                return;
            case Directions.Down:
                faceDirection = Vector3.down;
                return;
            case Directions.Left:
                faceDirection = Vector3.left;
                return;
            case Directions.Backward:
                faceDirection = Vector3.back;
                return;
            case Directions.Forward:
                faceDirection = Vector3.forward;
                return;
            case Directions.None:
                faceDirection = Vector3.zero;
                return;
            case Directions.Free:
                faceDirection = freeDirection;
                return;
        }
    }

    private void ResetMovement()
    {
        enemyMovment = Vector3.zero;
        moveDirection = _moveDirection;
        freeDirection = _freeDirection;
        SetDirection(moveDirection);
    }

    private void OnDisable()
    {
        gamePlay.EventResetEnemys -= ResetMovement;
    }
}
