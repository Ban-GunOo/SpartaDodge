using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f; // �̵� �ӵ�
        private Vector2 movement; // �̵� ����

        private void Update()
        {
            Move(); // �̵� ó��
        }

        private void Move()
        {
            // ����Ű �Է°� �ޱ�
            float moveX = Input.GetKey(KeyCode.RightArrow) ? 1 : (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
            float moveY = Input.GetKey(KeyCode.UpArrow) ? 1 : (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
            Debug.Log($"Move X: {moveX}, Move Y: {moveY}");
            // �̵� ���� ���� ����
            movement = new Vector2(moveX, moveY).normalized; // ���� ���� ����ȭ

            // Transform�� ����Ͽ� ��ġ ����
            transform.position += (Vector3)movement * moveSpeed * Time.deltaTime; // �ӵ� ����
        }
    }
}

