using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainScript10709 : MonoBehaviour
{
    // 2차원 배열을 인스펙터 창에서 다루기 위한 클래스
    [System.Serializable]
    public class CreateArray
    {
        public string[] array;
    }

    [SerializeField] int H;
    [SerializeField] int W;
    [SerializeField] CreateArray[] matrix;
    [SerializeField] GameObject SectorPrefab;
    [SerializeField] GameObject CloudPrefab;
    [SerializeField] GameObject Layout;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Show()
    {
        // GLG의 열을 W로 맞추고 Instantiate로 구역 프리팹 복제
        Layout.GetComponent<GridLayoutGroup>().constraintCount = W;
        for (int i = 0; i < H * W; i++)
        {
            Instantiate(SectorPrefab, Layout.transform.position, transform.rotation, Layout.gameObject.transform);
        }

        // 구름이 껴있는 구역에 구름 프리팹 복제해서 덧붙이기
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                if (matrix[i].array[j] == "c")
                {
                    Instantiate(CloudPrefab, Layout.transform.GetChild(W * i + j).transform.position, transform.rotation, Layout.gameObject.transform.GetChild(W * i + j));

                }
            }
        }

        // 구름이 껴있는 섹터의 값을 0으로 초기화
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                if (matrix[i].array[j] == "c")
                {
                    Layout.transform.GetChild(W * i + j).gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = 0.ToString();
                }
            }
        }

    }

    public void Play()
    {
        for (int i = H - 1; i >= 0; i--)
        {
            for (int j = W - 1; j >= 0; j--)
            {
                if (matrix[i].array[j] == "c")
                {
                    // 구름 프리팹 삭제 및 배열의 내용 변경(구름이 이미 지나갔으니까)
                    Destroy(Layout.transform.GetChild(W * i + j).gameObject.transform.GetChild(1).gameObject);
                    matrix[i].array[j] = ".";

                    // 아직 구름이 지나가지 않은 구역의 경우, text의 내용을 count로 변경
                    int current_count = int.Parse(Layout.transform.GetChild(W * i + j).gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text);
                    if (current_count == -1)
                    {
                        Layout.transform.GetChild(W * i + j).gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = count.ToString();
                    }

                    // 구름이 지나갈 다음 자리에 구름을 Instantiate로 다시 복제 및 배열 내용 변경
                    if (j < W - 1)
                    {
                        Instantiate(CloudPrefab, Layout.transform.GetChild(W * i + j + 1).transform.position, transform.rotation, Layout.gameObject.transform.GetChild(W * i + j + 1));
                        matrix[i].array[j + 1] = "c";
                    }

                }
            }
        }
        count += 1;
    }
}
