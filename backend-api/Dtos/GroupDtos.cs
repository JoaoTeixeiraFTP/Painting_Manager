namespace PaintingManager.Api.Dtos
{
    public class GroupDto
    {
        public int id { get; set; }
        public string? name { get; set; }
    }

    public class CreateGroupDto
    {
        public string name { get; set; } = string.Empty;
    }

    public class UpdateGroupDto
    {
        public string name { get; set; } = string.Empty;
    }
}
