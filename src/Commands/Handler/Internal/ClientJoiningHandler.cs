using CSM.Commands.Data.Internal;
using CSM.Networking;
using CSM.Networking.Status;
using CSM.Helpers;

namespace CSM.Commands.Handler.Internal
{
    public class ClientJoiningHandler : CommandHandler<ClientJoiningCommand>
    {
        public ClientJoiningHandler()
        {
            TransactionCmd = false;
        }

        protected override void Handle(ClientJoiningCommand command)
        {
            if (command.JoiningFinished)
            {
                MultiplayerManager.Instance.UnblockGame();
            }
            else
            {
                SpeedPauseHelper.PlayPauseRequest(true, 0, -1); //Pauses Game on Player joining
                MultiplayerManager.Instance.BlockGame(command.JoiningUsername);
            }
        }

        public override void OnClientDisconnect(Player player)
        {
            if (player.Status != ClientStatus.Connected)
            {
                Command.SendToClients(new ClientJoiningCommand
                {
                    JoiningFinished = true,
                    JoiningUsername = player.Username
                });
                MultiplayerManager.Instance.UnblockGame();
            }
        }
    }
}
