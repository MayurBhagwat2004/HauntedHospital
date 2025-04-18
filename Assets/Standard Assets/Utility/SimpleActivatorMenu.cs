using System;
using UnityEngine;
using UnityEngine.UI; // For using UI.Text components

namespace UnityStandardAssets.Utility
{
    public class SimpleActivatorMenu : MonoBehaviour
    {
        // A simple menu to toggle GameObjects in the scene
        public Text camSwitchButton; // Reference to a UI Text component
        public GameObject[] objects; // Array of GameObjects to toggle

        private int m_CurrentActiveObject;

        private void OnEnable()
        {
            // Start with the first object active
            m_CurrentActiveObject = 0;
            UpdateActiveObject();
        }

        public void NextCamera()
        {
            // Calculate the index of the next object
            int nextActiveObject = m_CurrentActiveObject + 1 >= objects.Length ? 0 : m_CurrentActiveObject + 1;

            // Set all objects inactive, except the next one
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(i == nextActiveObject);
            }

            // Update the current active object
            m_CurrentActiveObject = nextActiveObject;
            UpdateActiveObject();
        }

        private void UpdateActiveObject()
        {
            // Update the UI text to reflect the active GameObject's name
            if (camSwitchButton != null)
            {
                camSwitchButton.text = objects[m_CurrentActiveObject].name;
            }
        }
    }
}
