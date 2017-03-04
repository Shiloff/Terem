namespace Business.DataAccess.Public.Directory
{
    public interface IDirectoryStorage
    {
        ISexDirectory Sex { get; }
        IAlcoholDirectory Alcohol { get; }
        ISmokeDirectory Smoke { get; }
        IAnimalDirectory Animal { get; }
        IInteresDirectory Interes { get; }
        IActivityDirectory Activity { get; }
    }
}