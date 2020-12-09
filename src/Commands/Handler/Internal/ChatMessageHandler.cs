using CSM.Commands.Data.Internal;
using CSM.Panels;
using ICities;
using UnityEngine;

namespace CSM.Commands.Handler.Internal
{
    public class ChatMessageHandler : CommandHandler<ChatMessageCommand>
    {
        private static ChirpPanel chirpPane;

        public ChatMessageHandler()
        {
            TransactionCmd = false;
        }

        protected override void Handle(ChatMessageCommand command)
        {
            chirpPane = GameObject.Find("ChirperPanel").GetComponent<ChirpPanel>();
            chirpPane.AddMessage(new ChirpMess(0, command.Username, command.Message));
            ChatLogPanel.PrintChatMessage(command.Username, command.Message);
        }
    }

    public class ChirpMess : IChirperMessage
    {
        public ChirpMess( uint sender, string senderN, string tex)
        {
            senderID = sender;
            senderName = senderN;
            text = tex;
        }
        public uint senderID { get; set; }
        public string text { get; set; }
        public string senderName { get; set; }
    }
}
