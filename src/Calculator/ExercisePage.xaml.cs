using System.Diagnostics;

namespace Calculator;

public partial class ExercisePage : ContentPage
{
	private List<Exercise> exercises;
	public Exercise currentExercise;
	private int exercisesCompleted;
    private int exercisesCorrect;

    private bool isRetry = false;

	public ExercisePage()
	{
        InitExercises();
	}
	public async Task InitExercises()
	{
        //Task.Run(async () => { await ExerciseManager.GenerateNewExercises(); }).Wait();

        var iExercises = await ExerciseManager.GetAll();
        if (!iExercises.Any())
        {
            var response = await ExerciseManager.GenerateNewExercises();

            //Trace.WriteLine(response);
            //Trace.WriteLine(response.RequestMessage);
            if (response.IsSuccessStatusCode)
                Trace.WriteLine("\tExercises successfully generated.");

            iExercises = await ExerciseManager.GetAll();
        }
        exercises = iExercises.ToList();

        Trace.WriteLine("num exercises: " + exercises.Count);

        exercisesCorrect = await ExerciseManager.GetExercisesCorrect();

        InitializeComponent();
        SetComponents();
    }

    private void SetComponents()
    {
        currentExercise = exercises[0];

        if (currentExercise == null) { Trace.WriteLine("current equation is null!");  return; }

        if (!isRetry) { exercisesCompleted = currentExercise.Id - 1; }
        
        CurrentId.Text = "Exercise Number " + currentExercise.Id;
        CurrentScore.Text = "Current Score: " + exercisesCorrect.ToString() + " / " + exercisesCompleted.ToString();
        setEquationLabel();
        randomizeAnswerPlacements();
        Answer1.IsVisible = true;
        Answer2.IsVisible = true;
        Answer3.IsVisible = true;
        Retry.IsVisible = false;
        Next.IsVisible = false;
    }

    private void setEquationLabel()
    {
        if (currentExercise == null) { return; }

        String eqString = currentExercise.Int1.ToString();
        switch (currentExercise.Operator)
        {
            case OpType.ADDITION:
                eqString += " + ";
                break;
            case OpType.SUBTRACT:
                eqString += " - ";
                break;
            case OpType.MULTIPLY:
                eqString += " * ";
                break;
            case OpType.DIVIDE:
                eqString += " / ";
                break;
        }
        eqString += currentExercise.Int2.ToString();
        eqString += " = ?";
        Equation.Text = eqString;
    }
	private void randomizeAnswerPlacements()
	{
        var rand = new Random(Guid.NewGuid().GetHashCode()); //generates new seed each time
        int placement = rand.Next(0, 3);
		switch (placement)
		{
            case 0:
				Answer1.Text = currentExercise.CorrectAnswer.ToString();
                Answer2.Text = currentExercise.WrongAnswer1.ToString();
                Answer3.Text = currentExercise.WrongAnswer2.ToString();
                break;
			case 1:
                Answer1.Text = currentExercise.WrongAnswer2.ToString();
                Answer2.Text = currentExercise.CorrectAnswer.ToString();
                Answer3.Text = currentExercise.WrongAnswer1.ToString();
                break;
			case 2:
                Answer1.Text = currentExercise.WrongAnswer1.ToString();
                Answer2.Text = currentExercise.WrongAnswer2.ToString();
                Answer3.Text = currentExercise.CorrectAnswer.ToString();
                break;
        }
    }
    public async void OnAnserSelect(object sender, EventArgs e)
    {
        Button answer = (Button)sender;
        if (answer.Text == currentExercise.CorrectAnswer.ToString())
        {
            if (!isRetry)
            {
                var response = await ExerciseManager.IncrementExercisesCorrect();
                exercisesCorrect = await ExerciseManager.GetExercisesCorrect();
            }
            Equation.Text = "Correct Answer!";
            Answer1.IsVisible = false;
            Answer2.IsVisible = false;
            Answer3.IsVisible = false;
            OnNext(this, null);
        }
        else
        {
            if (!isRetry)
            {
                exercisesCompleted++;
            }
            Equation.Text = "Incorrect Answer...";
            Answer1.IsVisible = false;
            Answer2.IsVisible = false;
            Answer3.IsVisible = false;
            Retry.IsVisible = true;
            Next.IsVisible = true;
        }
        
    }
    public void OnRetry(object sender, EventArgs e)
    {
        isRetry = true;
        SetComponents();
    }
    public async void OnNext(object sender, EventArgs e)
    {
        isRetry = false;
        if (sender == this) // called internally/automatically
        {
            await Task.Delay(1500); // wait before continuing
        }

        if (exercises.Count <= 1)
        {
            var response = await ExerciseManager.GenerateNewExercises();
            //Trace.WriteLine(response);
            //Trace.WriteLine(response.RequestMessage);
            if (response.IsSuccessStatusCode) 
                Trace.WriteLine("\tExercises successfully generated.");
        }
        var delResponse = await ExerciseManager.DeleteExercise(currentExercise.Id);
        Trace.WriteLine("delete response: " + delResponse);

        var iExercises = await ExerciseManager.GetAll();
        exercises = iExercises.ToList();

        SetComponents();
    }
}