using System.Collections;
using AudioManagement;
using PersistentManager;
using Unity.VisualScripting;

namespace Minigames.Blackjack
{
    public class DealerDialogueController : DialogueController
    {
        public IEnumerator DisplayLine(string line)
        {
            yield return DisplayDialogue(new[] { line });
        }
    }
}
