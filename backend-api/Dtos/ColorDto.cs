namespace PaintingManager.Api.Dtos.Colors
{
    public class CreateColorDto
    {
        public int GroupId { get; set; }
        public string code { get; set; } = string.Empty;
        public string? Name { get; set; }
    }

    public class UpdateColorDto
    {
        public int GroupId { get; set; }
        public string code { get; set; } = string.Empty;
        public string? Name { get; set; }
    }

    public class ColorDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string code { get; set; } = string.Empty;
        public string? Name { get; set; }
    }
}
