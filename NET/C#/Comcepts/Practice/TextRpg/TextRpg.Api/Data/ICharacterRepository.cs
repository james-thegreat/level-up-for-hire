using TextRpg.Domain;

namespace TextRpg.Api.Data;

public interface ICharacterRepository
{
    Character Add(Character character);
    Character? GetById(Guid id);

    void Update(CharactersStore character);
}
