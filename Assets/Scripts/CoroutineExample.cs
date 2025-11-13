using System.Collections;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ExampleCoroutine()); //注意不是直接呼叫ExampleCoroutine()
    }

    IEnumerator ExampleCoroutine() 
    {
        Debug.Log("start coroutine!");
        yield return null; // Wait for one frame，注意這裡的yield return null不是return null ，yield return 表示其他程式會繼續執行
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        Debug.Log("Do linear intepretation");
        Vector2 startPos = transform.position;
        Vector2 targetPos = startPos + new Vector2(0, 5);
        float duration = 3f; // Duration of the movement in seconds
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector2.Lerp(startPos, targetPos, elapsed / duration); //插值，根據經過的時間計算位置
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }
}
