namespace Exercises.DB;

public enum OpType
{
    ADDITION,
    SUBTRACT, 
    MULTIPLY, 
    DIVIDE
}
public record Exercise
{
    public int Id { get; set; }
    public int Int1 { get; set; }
    public OpType Operator { get; set; }
    public int Int2 { get; set; }
    public int CorrectAnswer { get; set; }
    public int WrongAnswer1 { get; set; }
    public int WrongAnswer2 { get; set; }
}
public class ExerciseDB
{
    private static List<Exercise> _exercises = new List<Exercise>();

    private static int exercisesCorrect = 0;
    public static List<Exercise> GetExercises()
    {
        return _exercises;
    }
    public static Exercise GetExercise(int id)
    {
        return _exercises.SingleOrDefault(exercise => exercise.Id == id);
    }

    public static List<Exercise> GenerateExercises(int generateNum)
    {
        int nextId = 1;
        if (_exercises.Count != 0)
        {
            nextId = _exercises[_exercises.Count - 1].Id + 1;
        }
        for (int i = nextId; i < nextId + generateNum; i++)
        {
            _exercises.Add(GenerateExercise(i));
        }
        return _exercises;
    }
    private static Exercise GenerateExercise(int id)
    {
        var rand = new Random(Guid.NewGuid().GetHashCode()); //generates new seed each time
        int opNum = rand.Next(0, 4);
        OpType op = OpType.ADDITION;
        int int1 = 0;
        int int2 = 0;
        int corrVal = 0;
        int wrongVal = 0;
        int wrongOpVal = 0;

        int wrongOp = rand.Next(0, 4);
        //while (wrongOp == opNum) { wrongOp = rand.Next(0, 4); } //so wrongOp isn't the same as op

        int wrongValMargin = rand.Next(-5,6);
        while (wrongValMargin == 0) { wrongValMargin = rand.Next(-5, 6); }

        switch (opNum)
        {
            case 0:
                op = OpType.ADDITION;
                int1 = rand.Next(-100, 101);
                int2 = rand.Next(0, 101);
                corrVal = int1 + int2;
                wrongVal = corrVal + wrongValMargin;

                switch (wrongOp)
                {
                    case 0 or 1: 
                        wrongOpVal = int1 - int2;
                        break;
                    case 2:
                        wrongOpVal = int1 * int2;
                        if (Math.Abs(wrongOpVal) > 100) { wrongOpVal = corrVal - wrongValMargin;  }
                        break;
                    case 3:
                        wrongOpVal = -corrVal;
                        break;
                }
                break;
            case 1:
                op = OpType.SUBTRACT;
                int1 = rand.Next(-100, 101);
                int2 = rand.Next(0, 101);
                corrVal = int1 - int2;
                wrongVal = corrVal + wrongValMargin;

                switch (wrongOp)
                {
                    case 0 or 1:
                        wrongOpVal = int1 + int2;
                        break;
                    case 2:
                        wrongOpVal = int1 * int2;
                        if (Math.Abs(wrongOpVal) > 100) { wrongOpVal = corrVal - wrongValMargin;  }
                        break;
                    case 3:
                        wrongOpVal = -corrVal;
                        break;
                }
                break;
            case 2:
                op = OpType.MULTIPLY;
                int1 = rand.Next(-10, 21);
                int2 = rand.Next(-10, 16);
                corrVal = int1 * int2;
                wrongVal = corrVal + wrongValMargin;

                switch (wrongOp)
                {
                    case 0:
                        wrongOpVal = int1 + int2;
                        break;
                    case 1:
                        wrongOpVal = int1 - int2;
                        break;
                    case 2:
                        wrongOpVal = int1 * (int2 + 1);
                        break;
                    case 3:
                        wrongOpVal = (int1 + 1) * int2;
                        break;
                }
                break;
            case 3:
                op = OpType.DIVIDE;
                //int1 = rand.Next(-100, 101);
                //do
                //{
                //    int2 = rand.Next(-15, 16);
                //} while (int2 == 0 || int1 % int2 != 0); //make sure int2 is divisible by int1 & not equal to 0
                ////probably could make this more efficient
                //corrVal = int1 / int2;

                do //this is the more efficient verion haha
                {
                    int2 = rand.Next(-15, 16);
                } while (int2 == 0);
                corrVal = rand.Next(-15, 16);
                int1 = int2 * corrVal;
                wrongVal = corrVal + wrongValMargin;

                switch (wrongOp)
                {
                    case 0:
                        wrongOpVal = -corrVal;
                        break;
                    case 1:
                        if (corrVal % wrongValMargin == 0) { wrongOpVal = corrVal / wrongValMargin; }
                        else { wrongOpVal = int1 - int2; }
                        break;
                    case 2:
                        wrongOpVal = corrVal * wrongValMargin;
                        break;
                    case 3:
                        if (int2 > 1) { wrongOpVal = int1 / (int2 - 1); }
                        else if (int2 >= -1) { wrongOpVal = -corrVal;  } //if dividend is -1 or 1
                        else { wrongOpVal = int1 / (int2 + 1); }
                        break;
                }
                break;
        }

        if (wrongOpVal == corrVal) //final guard
        {
            wrongOpVal = corrVal - wrongValMargin;
        }

        return new Exercise { Id = id, Int1 = int1, Operator = op, Int2 = int2, CorrectAnswer = corrVal, WrongAnswer1 = wrongVal, WrongAnswer2 = wrongOpVal };
    }
    public static Exercise CreateExercise(Exercise newExercise)
    {
        //var matchingEx = _exercises.FirstOrDefault(ex => ex.Id == exercise.Id);
        _exercises.ForEach(exercise =>
        {
            if (exercise.Id == newExercise.Id)
            {
                //Exercise ID already exists, what's proper handling (replace, change id, throw error)
                return;
            }
        });
        _exercises.Add(newExercise);
        return newExercise;
    }
    public static void UpdateExercise(Exercise upExercise)
    {
        _exercises.Select(exercise =>
        {
            if (exercise.Id == upExercise.Id)
            {
                return upExercise; //if ids are equal, return updated exercise
            }
            return exercise;
        });
        //return upExercise;
    }
    public static void RemoveExercise(int id)
    {
        _exercises = _exercises.FindAll(exercise => exercise.Id != id).ToList();
    }

    public static int IncrementExercisesCorrect()
    {
        exercisesCorrect++;
        return exercisesCorrect;
    }
    public static int GetExercisesCorrect()
    {
        return exercisesCorrect;
    }
}
