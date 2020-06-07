namespace Sales.Infrastructure
{
    using ViewModels;
    public class Instancelocator
    {
        public MainViewModels Main { get; set; }

        public Instancelocator()
        {
            this.Main = new MainViewModels();
        }

    }
}
