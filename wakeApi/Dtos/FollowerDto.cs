namespace wakeApi.Dtos
{
    public class FollowerDto
    {
        public int Id { get; set; }
        public string FollowerName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public virtual UserDto? User { get; set; }
    }
}
