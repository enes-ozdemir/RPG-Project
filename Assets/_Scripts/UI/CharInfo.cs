﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class CharInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshPro levelText;
        [SerializeField] private TextMeshPro nameText;
        [SerializeField] private TextMeshPro dialogText;

        private void Start()
        {
            dialogText.gameObject.SetActive(false);
        }

        public void CreateCharInfo(int level,string name)
        {
            levelText.text = level.ToString();
            nameText.text = name;
        }
        
        private IEnumerator Talk_Co(string dialogue, float time)
        {
            dialogText.gameObject.SetActive(true);
            dialogText.text = dialogue;
            yield return new WaitForSeconds(time);
            dialogText.gameObject.SetActive(false);

        }
        
        public void Talk(string dialogue, float time)
        {
            if(gameObject.activeInHierarchy)
            {
                StartCoroutine(Talk_Co(dialogue, time));
            }
        }

    }
}