using SQLite;

namespace Calculator.ViewModel
{
    public partial class EquationViewModel : ObservableObject
    {
        private SQLiteAsyncConnection _conn;
        private string _dbPath;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;

        public ObservableCollection<Equation> Equations { get; private set; } = new();
        public EquationViewModel(string dbPath)
        {
            _dbPath = dbPath;
        }
        public async Task Init(bool reset = false)
        {
            if (_conn != null && !reset)
                return;

            _conn = new SQLiteAsyncConnection(_dbPath);
            await _conn.CreateTableAsync<Equation>();
        }

        public async Task UpdateEquations()
        {
            try
            {
                await Init();
                Equations = new ObservableCollection<Equation>(await _conn.Table<Equation>().ToListAsync());
                //Trace.WriteLine(Equations[Equations.Count - 1].Expression);
            }
            catch (Exception ex)
            {
                //StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
                Trace.WriteLine(string.Format("Failed to retrieve data. {0}", ex.Message));
            }
        }
        public async Task AddNewEquation(string expression, string answer)
        {
            int result = 0;
            try
            {
                await Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(expression) || string.IsNullOrEmpty(answer)) {
                    throw new Exception("Valid expression/answer required");
                }

                result = await _conn.InsertAsync(new Equation { Id = Equations.Count + 1, Expression = expression, Answer = answer });
                await UpdateEquations();
            }
            catch (Exception ex)
            {
                //StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
                Trace.WriteLine(string.Format("Failed to add equation. {0}", ex.Message));
            }
        }
        public async Task ClearEquationAsync()
        {
            await Init();
            await _conn.DeleteAllAsync<Equation>();
            Equations.Clear();
            await _conn.ExecuteAsync("delete from sqlite_sequence where name = 'Equation';");
        }
    }
}
