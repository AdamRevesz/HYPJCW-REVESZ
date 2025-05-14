// SalesItemShortViewDto.cs
using Models.Dtos.UserDto;

namespace Models.Dtos.SalesItem
{
    public class SalesItemShortViewDto
    {
        public string Id { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ApprovalRate { get; set; } = string.Empty;
        public UserShortViewDto Owner { get; set; } = new UserShortViewDto();
        public string Type { get; set; }
        public int Price { get; set; }
        public bool InStock { get; set; }
        public int Number { get; set; }
    }
}

