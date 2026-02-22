using AudioManagement;
using PersistentManager;
using Unity.VisualScripting;

namespace Minigames.Blackjack
{
    public class DealerDialogueController : DialogueController
    {
        public void DisplayLine(string line)
        {
            DisplayDialogue(new[] { line });
        }
    }
}
