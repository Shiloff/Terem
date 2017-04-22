namespace Business.DataAccess.Public.Services.Contact
{
    public class ContactFilter
    {
        public ContactFilter(int? sexId = null, int? sexWhoId = null, int? alcoholId = null, int? animalId = null, int? smokeId = null,
            int? activityId = null)
        {
            ActivityId = activityId;
            AnimalId = animalId;
            SmokeId = smokeId;
            SexId = sexId;
            SexWhoId = sexWhoId;
            AlcoholId = alcoholId;
        }

        public int? SexId { get; }
        public int? SexWhoId { get; }
        public int? AlcoholId { get; }
        public int? AnimalId { get; }
        public int? SmokeId { get; }
        public int? ActivityId { get; }
    }
}