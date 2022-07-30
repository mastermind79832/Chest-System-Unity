using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChestSystem.Core;

namespace ChestSystem.UI
{
    public class UIService : MonoSingletonGeneric<UIService>
    {
        public ModalWindow ModalWindow;
        public SlotManager SlotManager;

        // Start is called before the first frame update
        void Start()
        {
            ModalWindow.ShowMessage("Welcome", "Time to start Exploring", "Lets GO!!");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
