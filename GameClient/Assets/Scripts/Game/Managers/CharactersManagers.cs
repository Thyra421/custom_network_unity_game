using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    private Character Find(CharacterData data) {
        switch (data.type) {
            case CharacterType.Player:
                if (PlayersManager.Current.MyPlayer.Id == data.id)
                    return PlayersManager.Current.MyPlayer;
                return PlayersManager.Current.Find(data.id);
            case CharacterType.NPC:
                return NPCsManager.Current.Find(data.id);
        }

        return null;
    }

    private void OnMessageChannel(MessageChannel messageChannel) {
        Character character = Find(messageChannel.character);

        if (character != null)
            character.Activity.Channel(messageChannel.activityName, messageChannel.ticks, messageChannel.intervalTimeInSeconds);
    }

    private void OnMessageCast(MessageCast messageCast) {
        Character character = Find(messageCast.character);

        if (character != null)
            character.Activity.Cast(messageCast.activityName, messageCast.castTimeInSeconds);
    }

    private void OnMessageStopActivity(MessageStopActivity messageStopActivity) {
        Character character = Find(messageStopActivity.character);

        if (character != null)
            character.Activity.StopActivity();
    }

    private void OnMessageTriggerAnimation(MessageTriggerAnimation messageTriggerAnimation) {
        Character character = Find(messageTriggerAnimation.character);

        if (character != null)
            character.CharacterAnimation.SetTrigger(messageTriggerAnimation.animationName);
    }

    private void OnMessageAddAlteration(MessageAddAlteration messageAddAlteration) {
        Alteration alteration = Resources.Load<Alteration>($"{SharedConfig.Current.AlterationsPath}/{messageAddAlteration.alteration.alterationName}");

        if (alteration == null)
            return;

        AlterationController alterationController = new AlterationController(alteration, messageAddAlteration.alteration.remainingDuration, messageAddAlteration.alteration.owner.id);
        Character character = Find(messageAddAlteration.alteration.target);

        if (character != null)
            character.Alterations.Add(alterationController);
    }

    private void OnMessageRefreshAlteration(MessageRefreshAlteration messageRefreshAlteration) {
        Alteration alteration = Resources.Load<Alteration>($"{SharedConfig.Current.AlterationsPath}/{messageRefreshAlteration.alteration.alterationName}");
        Character character = Find(messageRefreshAlteration.alteration.target);

        if (alteration != null && character != null)
            character.Alterations.Refresh(alteration, messageRefreshAlteration.alteration.remainingDuration, messageRefreshAlteration.alteration.owner.id);
    }

    private void OnMessageRemoveAlteration(MessageRemoveAlteration messageRemoveAlteration) {
        Alteration alteration = Resources.Load<Alteration>($"{SharedConfig.Current.AlterationsPath}/{messageRemoveAlteration.alteration.alterationName}");
        Character character = Find(messageRemoveAlteration.alteration.target);

        if (alteration != null && character != null)
            character.Alterations.Remove(alteration, messageRemoveAlteration.alteration.owner.id);
    }

    private void OnMessageHealthChanged(MessageHealthChanged messageHealthChanged) {
        Character character = Find(messageHealthChanged.character);

        if (character != null) {
            character.Health.MaxHealth = messageHealthChanged.maxHealth;
            character.Health.CurrentHealth = messageHealthChanged.currentHealth;
        }
    }

    private void Awake() {
        TCPClient.MessageHandler.AddListener<MessageHealthChanged>(OnMessageHealthChanged);
        TCPClient.MessageHandler.AddListener<MessageChannel>(OnMessageChannel);
        TCPClient.MessageHandler.AddListener<MessageCast>(OnMessageCast);
        TCPClient.MessageHandler.AddListener<MessageStopActivity>(OnMessageStopActivity);
        TCPClient.MessageHandler.AddListener<MessageTriggerAnimation>(OnMessageTriggerAnimation);
        TCPClient.MessageHandler.AddListener<MessageAddAlteration>(OnMessageAddAlteration);
        TCPClient.MessageHandler.AddListener<MessageRefreshAlteration>(OnMessageRefreshAlteration);
        TCPClient.MessageHandler.AddListener<MessageRemoveAlteration>(OnMessageRemoveAlteration);
    }
}