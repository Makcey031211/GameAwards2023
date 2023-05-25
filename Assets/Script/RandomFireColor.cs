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
    [SerializeField]
    private string particleShaderProperty;
    [SerializeField]
    private string trailShaderProperty;

    private int colorNum;
    MaterialPropertyBlock materialPropertyBlock;

    // Start is called before the first frame update
    void Start()
    {
        materialPropertyBlock = new MaterialPropertyBlock();

        colorNum = UnityEngine.Random.Range(0, particleColor.Length);

        Debug.Log(colorNum);

        for (int i = 0; i < particles.Length; i++) {
            int color = Shader.PropertyToID(particleShaderProperty);
            materialPropertyBlock.SetColor(color, particleColor[colorNum]);
            particles[i].GetComponent<ParticleSystemRenderer>().SetPropertyBlock(materialPropertyBlock);
        }

        for (int i = 0; i < trails.Length; i++) {
            int color = Shader.PropertyToID(trailShaderProperty);
            materialPropertyBlock.SetColor(color, trailColor[colorNum]);
            trails[i].GetComponent<ParticleSystemRenderer>().SetPropertyBlock(materialPropertyBlock);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
