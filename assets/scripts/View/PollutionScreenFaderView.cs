using UnityEngine;
using System.Collections;

namespace Industree.View
{
    public class PollutionScreenFaderView : MonoBehaviour
    {
        public Color cleanColor;
        public Color pollutedColor;

        private Light sun;
        private Pollutable pollutable;

        private void Awake()
        {
            sun = GameObject.FindGameObjectWithTag(Tags.light).GetComponent<Light>();
            pollutable = (Pollutable)GameObject.FindObjectOfType(typeof(Pollutable));
        }

        private void Update()
        {
            sun.color = Color.Lerp(cleanColor, pollutedColor, ((float)pollutable.currentPollution) / pollutable.maxPollution);
        }
    }
}