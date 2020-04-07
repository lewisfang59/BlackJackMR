using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class ChipManager : MonoBehaviour
    {
        public const string CHIP_PREFAB_FOLDER = "ChipsPrefab";
        public const string ONE_DOLAR_CHIP = CHIP_PREFAB_FOLDER + "oneDolarChip";
        public const string FIVE_DOLAR_CHIP = CHIP_PREFAB_FOLDER + "fiveDolarChip";
        public const string TWENTY_FIVE_DOLAR_CHIP = CHIP_PREFAB_FOLDER + "twentyFiveDolarChip";
        public const string ONE_HUNDRED_DOLAR_CHIP = CHIP_PREFAB_FOLDER + "oneHundredDolarChip";

        int[] chipSelections = { 1, 5, 25, 100 };
        int[] chipsNeeded = { 0, 0, 0, 0 };

        public ChipManager()
        {
            
        }

        public List<GameObject> calculateChips(int value)
        {
            List<GameObject> chips = null;

            if(value <= 0)
            {
                throw new ArgumentException(String.Format("Chip value has to be greater than 0: {0}", value));
            }

            for(int i = chipSelections.Length; i < 0; i--)
            {
                if(chipSelections[i] <= value)
                {
                    chipsNeeded[i] = value / chipSelections[i];
                    value = value % chipSelections[i];
                }
            }

            return chips;
        }
        


    }
}
