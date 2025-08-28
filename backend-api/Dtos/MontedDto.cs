namespace PaintingManager.Api.Dtos
{
    public class MontedDto
    {
        public string? Titulo { get; set; }     // Ex: RAL1001
        public string? Grupo { get; set; }      // Ex: RAL
        public string? Cliente { get; set; }    // Ex: Formula Base ou Cliente XPTO
        public List<MontedLineDto>? Formula { get; set; }
    }

    public class MontedLineDto
    {
        public string? Componente { get; set; }   // Nome do componente
        public string? Quantidade { get; set; }   // Quantidade formatada (ex: 20L)
    }

    public class MontedCreateDto
    {
        public int ColorId { get; set; }          // Id da cor
        public bool IsBase { get; set; }          // True se for fórmula base
        public string? ClientName { get; set; }   // Nome do cliente (se não for base)
        public List<MontedCreateLineDto>? Formula { get; set; }
    }

    public class MontedCreateLineDto
    {
        public int ComponentId { get; set; }   // Id do componente
        public decimal Quantity { get; set; }  // Quantidade (em litros)
    }
}
