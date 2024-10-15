using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f; // 이동 속도
        private Vector2 movement; // 이동 방향

        private void Update()
        {
            Move(); // 이동 처리
        }

        private void Move()
        {
            // 방향키 입력값 받기
            float moveX = Input.GetKey(KeyCode.RightArrow) ? 1 : (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
            float moveY = Input.GetKey(KeyCode.UpArrow) ? 1 : (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
            Debug.Log($"Move X: {moveX}, Move Y: {moveY}");
            // 이동 방향 벡터 생성
            movement = new Vector2(moveX, moveY).normalized; // 방향 벡터 정규화

            // Transform을 사용하여 위치 변경
            transform.position += (Vector3)movement * moveSpeed * Time.deltaTime; // 속도 설정
        }
    }
}

