using UnityEngine;
using System.Collections;

public class EnemyCreator : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPointer;
    GameObject pointsStored;

    public void BuildEnemy()
    {
        pointsStored = GameObject.FindGameObjectWithTag("AiPoints");
        Transform newEnemyPointer = (Transform)Instantiate(enemyPointer);
        newEnemyPointer.position = Vector3.zero;
        GameObject newEnemy = (GameObject) Instantiate(enemy, Vector3.zero, Quaternion.identity);
        newEnemy.transform.SetParent(GameObject.FindGameObjectWithTag("EnemyTab").transform);
        newEnemyPointer.SetParent(pointsStored.transform);
        newEnemy.GetComponent<AiIntermediate>().targetObject = newEnemyPointer;
    }
}
