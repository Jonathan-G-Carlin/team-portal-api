namespace TeamPortal.Api.Dtos
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string FamilyName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
