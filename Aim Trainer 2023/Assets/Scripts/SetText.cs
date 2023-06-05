using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    public GameObject manager;
    private TMP_Text m_TextComponent;
    // Start is called before the first frame update
    void Awake()
    {
        m_TextComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_TextComponent.text = string.Format("Score: {0:0.00}\nTime Left: {1:0.00}\n\n", manager.GetComponent<SevenBALLS>().getScore(), manager.GetComponent<SevenBALLS>().getTimeLeft());
        List<int> sessionScores = manager.GetComponent<SevenBALLS>().getScores();
        List<float> sessionAccuracies = manager.GetComponent<SevenBALLS>().getAccuracies();
        for(int i = 0; i < sessionScores.Count; i++)
        {
            m_TextComponent.text += string.Format("Round {0}, Score: {1:0.00}, Accuracy: {2:0}%\n", i + 1, sessionScores[i], sessionAccuracies[i]*100);
        }
    }
}
