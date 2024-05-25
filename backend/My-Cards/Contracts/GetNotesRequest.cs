namespace My_Cards.Contracts
{
    public record GetNotesRequest();
    public record NoteDto(Guid id, string title, string description, DateTime time);
}
