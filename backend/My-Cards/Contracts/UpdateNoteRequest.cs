namespace My_Cards.Contracts
{
    public record UpdateNoteRequest(Guid id, string title, string description);
}
