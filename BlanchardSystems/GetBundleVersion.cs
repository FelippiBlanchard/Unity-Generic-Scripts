using TMPro;
using UnityEngine;

namespace BlanchardSystems
{
    public class GetBundleVersion : MonoBehaviour
    {
        private void OnEnable(){
            GetComponent<TextMeshProUGUI>().text = Application.version;
        }
    }
}
