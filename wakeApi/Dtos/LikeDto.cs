namespace wakeApi.Dtos
{
    public class LikeDto
    {
        public int Id { get; set; }
        public int CountLike { get; set; }
        public int CountDislike { get; set; }
        public int UserId { get; set; }
        public virtual UserDto? User { get; set; }
        public int PostId { get; set; }
        public virtual PostVideoDto? PostVideo { get; set; }
    }
}
