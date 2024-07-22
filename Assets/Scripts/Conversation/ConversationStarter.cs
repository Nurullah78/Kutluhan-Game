using UnityEngine;
//using DialogueEditor;

namespace Conversation
{
    public class ConversationStarter : MonoBehaviour
    {

//---------------Nesneler---------------

        //[SerializeField] NPCConversation myConversation;


//---------------Metotlar---------------

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //ConversationManager.Instance.StartConversation(myConversation);
                }
            }
        }
    }
}
