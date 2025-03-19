using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int Health = 100;            //ü���� ���� �Ѵ�. (����)
    public float Timer = 1.0f;          //Ÿ�̸Ӹ� ���� �Ѵ�.
    public int AttackPoint = 10;        //���ݷ��� ���� �Ѵ�

    // ù ������ ������ �ѹ� ���� �ȴ�.
    void Start()
    {
        Health = 100;                      //ù ������ ������ ü���� 100���� ���� ���ش�.
    }

    // �Ź� ������ �� ȣ�� �ȴ�.
    void Update()
    {
        CharacterHealthUp();
        CheckDeath();                  //�Լ� ȣ��
    }

    void CharacterHealthUp()
    {
        Timer -= Time.deltaTime;

        if(Timer <= 0)
        {
            Timer = 1.0f;
            Health += 20;
        }
    }

    public void CharacterHit(int Damage)          //Ŀ���� �������� �޴� �Լ��� ����Ѵ�.
    {
        Health -= Damage;                  //���� ���ݷ¿� ���� ü���� ���� ��Ų��.
    }

    void CheckDeath()                //�Լ� ����
    {
        if (Health <= 0)              //ü���� 0���Ϸ� �������� �ı� ��Ų��.
            Destroy(gameObject);      //�� ������Ʈ�� �ı��Ѵ�.
    }
}
