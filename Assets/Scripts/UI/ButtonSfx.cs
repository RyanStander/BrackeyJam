using AudioManagement;
using PersistentManager;
using UnityEngine;

namespace UI
{
    public class ButtonSfx : MonoBehaviour
    {
        public void ClickSfx()
        {
            AudioManager.PlayOneShot(AudioDataHandler.UI.ButtonClick());
        }
    }
}
