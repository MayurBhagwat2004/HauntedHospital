using System;
using UnityEngine;
using UnityEngine.UI; // For using Unity UI Text

namespace UnityStandardAssets.Utility
{
    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 0.5f; // Time period to measure FPS
        private int m_FpsAccumulator = 0;    // Accumulator for counting frames
        private float m_FpsNextPeriod = 0;   // Time to reset the FPS counter
        private int m_CurrentFps;            // Current FPS value
        const string display = "{0} FPS";    // FPS display format

        public Text fpsText; // Public reference to a UI Text component

        private void Start()
        {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;

            // Validate that fpsText is assigned
            if (fpsText == null)
            {
                Debug.LogError("FPSCounter: No Text component assigned for displaying FPS.");
            }
        }

        private void Update()
        {
            // Measure average frames per second
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
            {
                m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;

                // Update the UI Text component
                if (fpsText != null)
                {
                    fpsText.text = string.Format(display, m_CurrentFps);
                }
            }
        }
    }
}
