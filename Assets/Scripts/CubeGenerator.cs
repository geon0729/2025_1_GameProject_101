using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int totalCubes = 10;
    public float cubeSpacing = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        GenCube();                     //함수를 호출 한다.
    }

    public void GenCube()
    {
        Vector3 myPosition = transform.position;

        GameObject firestCube = Instantiate(cubePrefab, myPosition, Quaternion.identity); //첫 번째 큐브 생성 (내 위치에)

        for(int i = 1; i < totalCubes; i++)                  //나머지 큐브들 생성
        {
            //내 위치에서 z축으로 일정 간격 떨어진 위치에 생성
            Vector3 position = new Vector3(myPosition.x, myPosition.y, myPosition.z + (i * cubeSpacing));
            Instantiate(cubePrefab, position, Quaternion.identity);             //큐브 생성
        }
    }
}
