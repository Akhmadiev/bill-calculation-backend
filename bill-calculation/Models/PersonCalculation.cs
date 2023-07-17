namespace bill_calculation.Models
{
    public class PersonCalculation
    {
        public Guid PersonId { get; set; }

        public string PersonName { get; set; }

        public List<PersonCalculationGroup> PersonCalculationGroups { get; set; }

        public decimal TotalPrice { get; set; }
    }

    public class PersonCalculationGroup
    {
        public Guid GroupId { get; set; }

        public string GroupName { get; set; }

        public decimal PersonPrice { get; set; }

        public decimal GroupPrice { get; set; }

        public int GroupCount { get; set; }
    }
}
