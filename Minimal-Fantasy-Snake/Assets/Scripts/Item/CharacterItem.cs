public class CharacterItem : Item
{
    private Character character;

    public void SetCharacterData(Character _character)
    {
        if (_character is CharacterWarrior)
            character = gameObject.AddComponent<CharacterWarrior>();
        else if (_character is CharacterRouge)
            character = gameObject.AddComponent<CharacterRouge>();
        else if (_character is CharacterWizard)
            character = gameObject.AddComponent<CharacterWizard>();

        character.SetCharacterData(_character.GetCharacterData());
    }

    public Character GetCharacter()
    {
        return character;
    }
}
