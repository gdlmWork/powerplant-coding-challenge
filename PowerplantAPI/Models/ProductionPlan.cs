namespace PowerplantAPI.Models
{
    public partial class ProductionPlan
    {
        public ProductionPlan (string name , double power)
        {
            Name = name;
            P = power;
        }
        public string Name { get; set; }
        public double P { get; set; }
    }
}
