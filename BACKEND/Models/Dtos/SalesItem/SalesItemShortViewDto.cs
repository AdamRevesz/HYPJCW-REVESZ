// SalesItemShortViewDto.cs
namespace Models.Dtos.SalesItem
{
    public class SalesItemShortViewDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ApprovalRate { get; set; } = string.Empty;
        public Models.User Owner { get; set; } = new Models.User();
        public string Category { get; set; } = string.Empty;
        public TypeOfItem Type { get; set; }
        public int Price { get; set; }
        public bool InStock { get; set; }
        public int Number { get; set; }
    }
}

