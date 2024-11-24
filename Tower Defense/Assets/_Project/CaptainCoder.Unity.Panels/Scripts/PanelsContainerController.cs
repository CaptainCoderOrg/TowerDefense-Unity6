using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using CaptainCoder.Unity.Extensions;

namespace CaptainCoder.Unity.Panels
{
    public sealed class PanelsContainerController : MonoBehaviour
    {
        [field: SerializeField]
        public List<PanelData> Panels { get; private set; }
        [field: SerializeField]
        public PanelController PanelPrefab { get; private set; }
        [field: SerializeField]
        public PanelIconController PanelIconPrefab { get; private set; }
        [field: SerializeField]
        public List<PanelController> PanelControllers { get; private set; }
        [field: SerializeField]
        public Transform PanelsPivot { get; private set; }
        [field: SerializeField]
        public Transform Icons { get; private set; }

        void Awake()
        {
            Initialize();
        }

        public void Toggle(int ix)
        {
            PanelControllers[ix].Toggle();
        }

        [Button("Initialize")]
        public void Initialize()
        {
            PanelsPivot.DestroyChildren();
            Icons.DestroyChildren();
            PanelIconPrefab.gameObject.SetActive(false);
            PanelPrefab.gameObject.SetActive(false);
            PanelControllers.Clear();
            foreach (PanelData panelData in Panels)
            {
                PanelController panelController = GameObject.Instantiate(PanelPrefab, PanelsPivot);
                panelController.name = $"{panelData.Name} (Panel)";
                GameObject.Instantiate(panelData.PanelPrefab, panelController.transform);
                
                PanelIconController icon = GameObject.Instantiate(PanelIconPrefab, Icons);
                icon.Icon = panelData.Icon;
                icon.name = $"{panelData.Name} (Button)";
                icon.Panel = panelController;

                PanelControllers.Add(panelController);
                panelController.gameObject.SetActive(true);
                icon.gameObject.SetActive(true);
            }
        }

    }
}