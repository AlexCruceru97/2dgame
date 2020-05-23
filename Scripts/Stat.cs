using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stat : MonoBehaviour
{
    //the actual image we are changing the fill of
    private Image content;

    [SerializeField]
    private Text statValue;

    //hold the current fill value so that we know if we need to lerp a value
    private float currentFill;

    private float currentValue;

    [SerializeField]
    private float lerpSpeed;

    public float MyMaxValue { get; set; }
    public float MyCurrentValue {
        get { 
            return currentValue; 
        }
        set {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }
           

            currentFill = currentValue / MyMaxValue;

            statValue.text = currentValue + "/" + MyMaxValue;
        } 

    }


    void Start()
    {
        content = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //smooth filling of health/mana bar
        if(currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
           
        }
        
    }


    //Initialize the health and mana bar
    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
