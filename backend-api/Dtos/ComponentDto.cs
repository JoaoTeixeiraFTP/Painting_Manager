namespace PaintingManager.Api.Dtos.Components
{
    public class CreateComponentDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateComponentDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class ComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
