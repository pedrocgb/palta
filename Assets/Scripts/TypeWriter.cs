using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NoHope.NPCs
{
    public static class TypeWriter
    {
        public static IEnumerator StartTyping(string _text, TextMeshProUGUI _label, float _timer)
        {
            _label.text = string.Empty;

            WaitForSeconds waitTimer = new WaitForSeconds(_timer);
            foreach (char c in _text)
            {
                _label.text = _label.text + c;
                yield return waitTimer;
            }
        }
    }
}