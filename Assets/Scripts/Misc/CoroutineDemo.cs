using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineDemo : MonoBehaviour
{

    private int secretNumber;

    public int MyNumber => secretNumber;
    public int GetNumber { get { return secretNumber; } }
    public int SecretNumber 
    { 
        get => secretNumber; 
        set { 
            value = Mathf.Clamp(value, 0, 10);
            secretNumber = value; 
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WhileRoutine());
        SecretNumber = 5;
        int s = SecretNumber;
    }


    IEnumerator SimpleCoroutine()
    {
        Debug.Log("A");
        yield return new WaitUntil(GetCondition);
        yield return Delay();
        Debug.Log("B");
    }

    void DoThing()
    {
        if(GetCondition())
            return;
    }
    IEnumerator WhileRoutine()
    {
        yield return new WaitWhile(() => !GetCondition());
        yield return null;
        yield return new WaitUntil(() => GetCondition());
        yield return null;
        while(true) //while
        {
            if(GetCondition()) //until this condition is true.
                break;
            Debug.Log("");
            yield return null;
        }
        Debug.Log("while was broken"); //wait
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
    }

    bool GetCondition()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

}
