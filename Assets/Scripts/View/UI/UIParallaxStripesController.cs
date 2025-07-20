using UnityEngine;
using UnityEngine.UI;

namespace Pong.View.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Image))]
    public class UIParallaxStripesController : MonoBehaviour
    {
        [Header("Color Settings")]
        public Color mainColor = new Color(0.1f, 0.3f, 0.5f, 1f);
        public Color secondColor = new Color(0.2f, 0.4f, 0.6f, 1f);
    
        [Header("Layer 1 Settings")]
        [Range(0f, 2f)]
        public float layer1Speed = 0.1f;
        [Range(1f, 50f)]
        public float layer1Scale = 10f;
        [Range(0f, 1f)]
        public float layer1Opacity = 1f;
    
        [Header("Layer 2 Settings")]
        [Range(0f, 2f)]
        public float layer2Speed = 0.2f;
        [Range(1f, 50f)]
        public float layer2Scale = 20f;
        [Range(0f, 1f)]
        public float layer2Opacity = 0.7f;
    
        [Header("Rotation Settings")]
        [Range(0f, 360f)]
        public float rotationAngle = 0f;
        public bool animateRotation = false;
        [Range(-50f, 50f)]
        public float rotationSpeed = 10f;
    
        [Header("Animation Settings")]
        public bool animateColors = false;
        public Color targetMainColor;
        public Color targetSecondColor;
        [Range(0.1f, 10f)]
        public float colorTransitionSpeed = 1f;
    
        private Material material;
        private Image imageComponent;
    
        void OnEnable()
        {
            imageComponent = GetComponent<Image>();
        
            if (Application.isPlaying)
            {
                if (imageComponent.material == null || imageComponent.material.name == "Default UI Material")
                {
                    Debug.LogError("Please assign a material with the UI/ParallaxStripes shader to the Image component");
                    return;
                }
            
                material = new Material(imageComponent.material);
                imageComponent.material = material;
            }
            else
            {
                material = imageComponent.material;
            }
        
            if (targetMainColor == Color.clear)
                targetMainColor = mainColor;
            if (targetSecondColor == Color.clear)
                targetSecondColor = secondColor;
            
            UpdateShaderProperties();
        }
    
        void Update()
        {
            if (material == null) return;
        
            if (animateColors && Application.isPlaying)
            {
                mainColor = Color.Lerp(mainColor, targetMainColor, Time.deltaTime * colorTransitionSpeed);
                secondColor = Color.Lerp(secondColor, targetSecondColor, Time.deltaTime * colorTransitionSpeed);
            
                if (Vector4.Distance(mainColor, targetMainColor) < 0.01f)
                {
                    targetMainColor = new Color(Random.value, Random.value, Random.value, 1f);
                }
                if (Vector4.Distance(secondColor, targetSecondColor) < 0.01f)
                {
                    targetSecondColor = new Color(Random.value, Random.value, Random.value, 1f);
                }
            }
        
            if (animateRotation && Application.isPlaying)
            {
                rotationAngle = (rotationAngle + rotationSpeed * Time.deltaTime) % 360f;
                if (rotationAngle < 0) rotationAngle += 360f;
            }
        
            UpdateShaderProperties();
        }
    
        void UpdateShaderProperties()
        {
            if (material != null)
            {
                // Update all shader properties
                material.SetColor("_MainColor", mainColor);
                material.SetColor("_SecondColor", secondColor);
            
                material.SetFloat("_Layer1Speed", layer1Speed);
                material.SetFloat("_Layer2Speed", layer2Speed);
            
                material.SetFloat("_Layer1Scale", layer1Scale);
                material.SetFloat("_Layer2Scale", layer2Scale);
            
                material.SetFloat("_Layer1Opacity", layer1Opacity);
                material.SetFloat("_Layer2Opacity", layer2Opacity);
            
                material.SetFloat("_RotationAngle", rotationAngle);
            }
        }
    
        public void SetTargetColors(Color newMainColor, Color newSecondColor, bool animate = true)
        {
            targetMainColor = newMainColor;
            targetSecondColor = newSecondColor;
            animateColors = animate;
        
            if (!animate)
            {
                mainColor = newMainColor;
                secondColor = newSecondColor;
            }
        }
    
        public void SetRotation(float angle, bool animate = false, float speed = 10f)
        {
            if (!animate)
            {
                rotationAngle = angle % 360f;
                animateRotation = false;
            }
            else
            {
                animateRotation = true;
                rotationSpeed = speed;
            }
        }
    }
}