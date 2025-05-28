using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider speedSlider;

    public Slider numsSlider1;
    public Slider numsSlider2;

    public Toggle checkTrail;

    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;

    public TMP_InputField inputField;

    public GameManager gameManager;

    public FractionBase base1;
    public FractionBase base2;
    void Start()
    {
        speedSlider.onValueChanged.AddListener(gameManager.SetSpeedDron);
        numsSlider1.onValueChanged.AddListener(base1.SetNumDrons);
        numsSlider2.onValueChanged.AddListener(base2.SetNumDrons);
        checkTrail.onValueChanged.AddListener(gameManager.SetTrail);
        inputField.onEndEdit.AddListener(OnInput);
    }

    void Update()
    {
        scoreText1.text = "Score " + base1.name + " :" + base1.score;
        scoreText2.text = "Score " + base2.name + " :" + base2.score;
    }

    void OnInput(string input)
    {
        float newDelay;
        if (float.TryParse(input, out newDelay))
        {
            gameManager.SetDelay(newDelay);
        }
        else
        {
            Debug.Log("Неверный формат частоты!");
        }

    }
}
