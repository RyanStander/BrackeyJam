using System.Collections;
using AudioManagement;
using PersistentManager;
using Unity.VisualScripting;

namespace Minigames.Blackjack
{
    public class DealerDialogueController : DialogueController
    {
        public IEnumerator DisplayLine(string line, float displayDuration = 2f)
        {
            _lineDisplayDuration = displayDuration;
            yield return DisplayDialogue(new[] { line });
        }
    }
}
