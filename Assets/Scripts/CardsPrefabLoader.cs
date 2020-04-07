using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class CardsPrefabLoader : MonoBehaviour
    {
        private UnityEngine.Object[] cards;
        private GameObject obj;

        private void Start()
        {
            cards = Resources.LoadAll("CardsPrefab", typeof(MeshRenderer));

            foreach (var t in cards)
            {
                Debug.Log(t.name);
            }

            Debug.Log(cards.Length);
        }
    }
}
