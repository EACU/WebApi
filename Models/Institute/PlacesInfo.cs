namespace EACA_API.Models.Institute
{
    public class PlacesInfo
    {
        public string Id { get; set; }

        public int? AllPlaces => BudgetPlaces + NotBudetPlaces;

        public int? BudgetPlaces => MainContestPlaces + SpecialQuotaPlaces + TargetPlaces;

        public int? MainContestPlaces { get; set; }
        public int? SpecialQuotaPlaces { get; set; }
        public int? TargetPlaces { get; set; }

        public int? NotBudetPlaces { get; set; }

        public double? CostSemestr { get; set; }
    }
}
