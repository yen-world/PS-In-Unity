using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.UI;
using System;

public class MainScript : MonoBehaviour
{
    [SerializeField] int N, M;
    [SerializeField] int J;
    [SerializeField] int[] pos;
    [SerializeField] GameObject BlockPrefab;
    [SerializeField] GameObject Blocks;
    [SerializeField] GameObject Basket;
    RectTransform BasketRect;
    [SerializeField] GameObject ApplePrefab;
    [SerializeField] GameObject ScoreText;
    BoxCollider2D col;
    int result = 0;
    int count = 0;
    bool appleFlag = true;
    float left, right;
    void Start()
    {
        BasketRect = Basket.GetComponent<RectTransform>();
    }

    void Update()
    {
        // 점수판 갱신
        ScoreText.GetComponent<TMP_Text>().text = result.ToString();
        try
        {
            while (appleFlag && count < J)
            {
                // 사과를 해당 위치에 복제
                Instantiate(ApplePrefab, Blocks.transform.GetChild(pos[count] - 1).gameObject.transform.position, transform.rotation, Blocks.transform.GetChild(pos[count] - 1).gameObject.transform);
                appleFlag = !appleFlag;

                // 바스켓의 왼쪽과 오른쪽의 가장 끝 자리 값 구하기
                left = BasketRect.anchoredPosition.x - (BasketRect.sizeDelta.x / 2);
                right = BasketRect.anchoredPosition.x + (BasketRect.sizeDelta.x / 2);

                // 복제된 사과가 떨어지는 위치의 x값 구하기
                float appleX = -800 + 320 * (pos[count] - 1);

                // 바스켓의 범위 안에 사과가 떨어지지 않는다면 바스켓의 위치를 옮기기
                if (left > appleX || right < appleX)
                {
                    BasketRect.anchoredPosition = new Vector3(160f * (M - 1) + appleX, BasketRect.anchoredPosition.y, 0);
                    result += 1;
                }
                count += 1;
            }
        }
        catch (UnityException)
        {

        }
    }
    public int Result
    {
        get { return result; }
        set { result = value; }
    }

    public bool Flag
    {
        get { return appleFlag; }
        set { appleFlag = value; }
    }

    public void GameStart()
    {
        // 바구니 활성화
        Basket.SetActive(true);

        // 사과 나오는 블록 오브젝트를 갯수만큼 복제
        for (int i = 0; i < N; i++)
        {
            Instantiate(BlockPrefab, Blocks.transform.position, transform.rotation, Blocks.transform);
        }

        // 사과 블록의 텍스트를 번호순으로 변경
        for (int i = 0; i < N; i++)
        {
            Blocks.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = (i + 1).ToString();
        }

        // Basket의 RectTransform 컴포넌트의 사이즈를 조절, 콜라이더도 조절, 조절된 크기에 맞춰 위치 조절
        BasketRect.sizeDelta = new Vector2(320f * M, BasketRect.rect.height);
        col = Basket.GetComponent<BoxCollider2D>();
        col.size = BasketRect.sizeDelta;
        BasketRect.anchoredPosition = new Vector3(BasketRect.anchoredPosition.x + 160f * (M - 1), BasketRect.anchoredPosition.y, 0);
    }
}

