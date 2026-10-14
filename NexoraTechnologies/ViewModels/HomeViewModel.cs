namespace NexoraTechnologies.ViewModels;

public class HomeViewModel
{
    public string CompanyName { get; set; } = "Nexora Technologies";
    public string Tagline { get; set; } = "Ideas Into Innovation";
    public List<(string Title, string Description, string Icon)> Services { get; set; } = new()
    {
        ("Website Development", "Fast, responsive websites built around your business goals.", "bi-globe2"),
        ("Software Development", "Custom business software that simplifies everyday work.", "bi-code-slash"),
        ("Mobile App Development", "Mobile experiences for customers, teams, and services.", "bi-phone"),
        ("ERP & Business Systems", "Connected workflows for operations, records, and reporting.", "bi-grid-1x2"),
        ("AI & Automation", "Practical automation to reduce repetitive work.", "bi-stars"),
        ("IT Support & Maintenance", "Ongoing improvements, maintenance, and technical support.", "bi-shield-check")
    };
}
