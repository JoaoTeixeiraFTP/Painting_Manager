namespace PaintingManager.Api.Dtos
{
    public class MontedFullCreateDto
    {
        public MontedColorDto Color { get; set; }    // Grupo + Cor
        public bool IsBase { get; set; }
        public string? ClientName { get; set; }
        public List<MontedFullLineDto> Formula { get; set; }
    }

    public class MontedColorDto
    {
        public string GroupName { get; set; }   // Ex: RAL
        public string Code { get; set; }        // Ex: RAL2001
        public string Name { get; set; }        // Ex: Laranja Vermelho
    }

    public class MontedFullLineDto
    {
        public string ComponentName { get; set; }
        public decimal Quantity { get; set; }
    }
}
