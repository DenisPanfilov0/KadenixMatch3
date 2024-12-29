using System;
using System.Linq;
using UnityEngine;

namespace Code.Config
{
    [CreateAssetMenu(fileName = "DebugGameSettings", menuName = "ECS Survivors/Debug Game Settings")]
    public class DebugGameSettings : ScriptableObject
    {
        [SerializeField] private bool _isProducSettings;
        
        [field: SerializeField] public bool FreeSwapWithoutMatches { get; private set; }
        [field: SerializeField] public ListAllSystems ListAllSystems { get; private set; }
        
        
        private void OnValidate()
        {
            if (_isProducSettings)
            {
                ResetAllSettings();
                _isProducSettings = false;
            }
        }

        private void ResetAllSettings()
        {
            var fields = GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Where(f => f.FieldType == typeof(bool));
            
            foreach (var field in fields) 
                field.SetValue(this, false);

            if (ListAllSystems != null)
            {
                var subFields = typeof(ListAllSystems).GetFields()
                    .Where(f => f.FieldType == typeof(bool));
                
                foreach (var subField in subFields) 
                    subField.SetValue(ListAllSystems, false);
            }
        }
    }

    [Serializable]
    public class ListAllSystems
    {
        public bool PowerUpGenerationSystem;
    }
}