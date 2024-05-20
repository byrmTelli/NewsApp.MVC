namespace NewsApp.CORE.ViewModels.AdminPageViewModels
{
    public class DashboardViewModel
    {
        public int UserCount { get; set; }
        public int ManagerCount { get; set; }
        public int PostCount { get; set; }
        public int WriterCount { get; set; }
        public List<int> PieChartData { get; set; }
        public List<string> PieChartTitles { get; set; }
        public List<int> BarChartData { get; set; }
        public List<string> BarChartTitles { get; set; }
    }
}
