namespace EVegaT5
{
    public partial class App : Application
    {
        public static PersonRepository pRepo { set; get; }
        public App(PersonRepository personRepository)
        {
            InitializeComponent();

            MainPage = new Views.vPersona();
            pRepo = personRepository;
        }
    }
}
