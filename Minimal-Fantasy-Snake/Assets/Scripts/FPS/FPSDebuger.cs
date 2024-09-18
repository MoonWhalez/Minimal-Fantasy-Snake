using TMPro;
using UnityEngine;

public class FPSDebuger : MonoBehaviour
{
    private float deltaTime;
    private float fps;

    TextMeshProUGUI TMPText;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out TMPText);
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = Mathf.RoundToInt(1.0f / deltaTime);

        TMPText.text = "FPS : " + fps.ToString();
    }
}
