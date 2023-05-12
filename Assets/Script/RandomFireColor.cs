using Unity.Mathematics;
using UnityEngine;

public class RandomFireColor : MonoBehaviour
{
    [ColorUsage(false, true), SerializeField]
    private Color[] particleColor;
    [ColorUsage(true, true), SerializeField]
    private Color[] trailColor;
    [SerializeField]
    private ParticleSystem[] particles;
    [SerializeField]
    private ParticleSystem[] trails;

    private int colorNum;

    // Start is called before the first frame update
    void Start()
    {
        colorNum = UnityEngine.Random.Range(0, particleColor.Length);

        Debug.Log(colorNum);


        for (int i = 0; i < particles.Length; i++) {
            int color = Shader.PropertyToID("Color_6e421ff9f89940d29c66c6f5baf1449f");
            particles[i].GetComponent<ParticleSystemRenderer>().material.SetColor(color, particleColor[colorNum]);
        }

        for (int i = 0; i < trails.Length; i++) {
            int color = Shader.PropertyToID("Color_27cb2d941779445c8e3e1604cb5274e6");
            trails[i].GetComponent<ParticleSystemRenderer>().material.SetColor(color, particleColor[colorNum]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
