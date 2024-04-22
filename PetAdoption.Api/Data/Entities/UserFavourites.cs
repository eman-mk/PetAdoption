namespace PetAdoption.Api.Data.Entities
{
    public class UserFavourite
    {
        public int UserId { get; set; }
        public int PetId { get; set; }

        public virtual User User { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
