using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.UI
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField]
        private SlotController[] slots;

        private SlotController currentUnlocking;
    }
}
