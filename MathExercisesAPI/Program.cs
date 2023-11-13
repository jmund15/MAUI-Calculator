using Microsoft.OpenApi.Models;
using Exercises.DB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "Keep track of your tasks", Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TODO API V1");
});

app.MapGet("/exercises/{id}", (int id) => ExerciseDB.GetExercise(id));
app.MapGet("/exercises", () => ExerciseDB.GetExercises());
app.MapPost("/exercises/{generateNum}", (int generateNum) => ExerciseDB.GenerateExercises(generateNum));
app.MapPost("/exercises", (Exercise exercise) => ExerciseDB.CreateExercise(exercise));
app.MapPut("/exercises", (Exercise exercise) => ExerciseDB.UpdateExercise(exercise));
app.MapDelete("/exercises/{id}", (int id) => ExerciseDB.RemoveExercise(id));

app.MapGet("/exercises/correct", () => ExerciseDB.GetExercisesCorrect());
app.MapPost("/exercises/correct", () => ExerciseDB.IncrementExercisesCorrect());

app.Run();
