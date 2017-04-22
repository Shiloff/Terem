namespace Business.DataAccess.Public.Services.Contact
{
    public class ContactFilter
    {
        public ContactFilter(int? sexId = null, int? alcoholId = null, int? animalId = null, int? smokeId = null,
            int? activityId = null)
        {
            ActivityId = activityId;
            AnimalId = animalId;
            SmokeId = smokeId;
            SexId = sexId;
            AlcoholId = alcoholId;
        }

        public int? SexId { get; }
        public int? AlcoholId { get; }
        public int? AnimalId { get; }
        public int? SmokeId { get; }
        public int? ActivityId { get; }
    }
}