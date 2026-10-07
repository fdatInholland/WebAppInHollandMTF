namespace WebAppInHollandMTF.DTO
{
    public class CreateNoteDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IFormFile? Attachment { get; set; }
    }
}
