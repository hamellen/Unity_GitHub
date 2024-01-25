
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FadeSystem : MonoBehaviour
{

    [SerializeField] private float fadetime;

    private Image image;

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void total_start() {

        Fadeout();
        Fadein();
    }
    public void Fadeout() {

        StartCoroutine(Fade(0, 1));
    }
    public void Fadein()
    {

        StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float start, float end) {

        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1) {

            currentTime += Time.deltaTime;
            percent = currentTime / fadetime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
    
    }
}
