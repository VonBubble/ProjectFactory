using Assets.Scripts.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ToolbarEntityButtons: MonoBehaviour
    {
        public GameObject button;
        public string[] entitiesName;

        private FactoryController factoryController;

        public void Start()
        {
            factoryController = FindObjectOfType<FactoryController>();
            if(factoryController == null)
            {
                Debug.Log("No Factory Controller component found. Make sure that one is present in the scene.");
                return;
            }

            foreach (var entityName in entitiesName)
            {
                var go = Instantiate(button, this.transform);
                var textLabel = go.GetComponentInChildren<Text>();
                if(textLabel != null)
                {
                    textLabel.text = entityName;
                }
                var buttonEvent = go.GetComponent<Button>();
                buttonEvent.onClick.AddListener(() => { factoryController.SetFactoryToSpawn(entityName); });
            }
        }
    }
}
