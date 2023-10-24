using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainScript9655 : MonoBehaviour
{
    [SerializeField] int N;
    [SerializeField] GameObject StronPrefab;
    [SerializeField] GameObject Board_GLG;
    [SerializeField] GameObject SK_GLG;
    [SerializeField] GameObject CY_GLG;
    [SerializeField] GameObject result_message;
    bool sk = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        for (int i = 0; i < N; i++)
        {
            Instantiate(StronPrefab, Board_GLG.transform.position, transform.rotation, Board_GLG.transform);
        }
    }
    public void GamePlay()
    {
        int rand_number = Random.Range(0, 100);
        rand_number = (rand_number > 49) ? 3 : 1; // 49보다 크면 3, 작으면 1
        if (N >= 3)
        {
            N -= rand_number;
            if (sk)
            {
                for (int i = 0; i < rand_number; i++)
                {
                    Instantiate(StronPrefab, SK_GLG.transform.position, transform.rotation, SK_GLG.transform);
                    Destroy(Board_GLG.transform.GetChild(i).gameObject);
                }
                sk = false;
            }
            else
            {
                for (int i = 0; i < rand_number; i++)
                {
                    Instantiate(StronPrefab, CY_GLG.transform.position, transform.rotation, CY_GLG.transform);
                    Destroy(Board_GLG.transform.GetChild(i).gameObject);
                }
                sk = true;
            }
        }
        else if (N >= 1)
        {
            N -= 1;
            if (sk)
            {
                Instantiate(StronPrefab, SK_GLG.transform.position, transform.rotation, SK_GLG.transform);
                Destroy(Board_GLG.transform.GetChild(0).gameObject);
                sk = false;
            }
            else
            {
                Instantiate(StronPrefab, CY_GLG.transform.position, transform.rotation, CY_GLG.transform);
                Destroy(Board_GLG.transform.GetChild(0).gameObject);
                sk = true;
            }
        }


        if (N == 0)
        {
            result_message.SetActive(true);
            if (sk)
            {
                result_message.GetComponent<TMP_Text>().text = "창영이가 이겼습니다!";
            }
            else
            {
                result_message.GetComponent<TMP_Text>().text = "상근이가 이겼습니다!";
            }
        }

    }
}


