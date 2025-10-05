using UnityEngine;
using UnityEngine.UI;

public class BarraScore : MonoBehaviour
{
    public Slider slider;

    public void AlternarScore (float score)
    {
        slider.value = score;
    }
  
}
