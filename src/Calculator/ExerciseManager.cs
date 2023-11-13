using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Calculator
{
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
    public static class ExerciseManager
    {
        static readonly string baseUrl = "http://localhost:5131";
        public static readonly string exercisesUrl = $"{baseUrl}" + "/exercises";
        public static readonly string exercisesCorrectUrl = $"{baseUrl}" + "/exercises/correct";


        public static JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        static HttpClient client = new HttpClient();

        public static async Task<IEnumerable<Exercise>> GetAll()
        {
            Trace.WriteLine("here");
            if (Connectivity.Current.NetworkAccess == NetworkAccess.None)
            {
                Trace.WriteLine("No connection, can't get exercises");
                return new List<Exercise>();
            }
            return await client.GetFromJsonAsync<IEnumerable<Exercise>>(exercisesUrl);
        }
        public static async Task<HttpResponseMessage> GenerateNewExercises()  
        {
            HttpResponseMessage response = null;

            if (Connectivity.Current.NetworkAccess == NetworkAccess.None)
            {
                Trace.WriteLine("No connection, can't generate exercises");
                return response;
            }

            try
            {
                var testEx = new Exercise { Id = 1, CorrectAnswer = 1, Int1 = 0, Int2 = 10, Operator = OpType.ADDITION, WrongAnswer1 = 0, WrongAnswer2 = 5465 };
                string jsonEx = JsonSerializer.Serialize(testEx, serializerOptions);
                //Trace.WriteLine(jsonEx);
                //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                StringContent content = new StringContent(jsonEx);

                Uri uri = new Uri(string.Format(exercisesUrl, 10));

                Trace.WriteLine("before await gen");

                response = await client.PostAsync(exercisesUrl + "/10", null);
                //Trace.WriteLine("before await gen");

                //Trace.WriteLine(response);
                //Trace.WriteLine(response.RequestMessage);
                //if (response.IsSuccessStatusCode)
                //    Trace.WriteLine("\tExercises successfully generated.");
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> DeleteExercise(int exId)
        {
            HttpResponseMessage response = null;
            if (Connectivity.Current.NetworkAccess == NetworkAccess.None)
            {
                Trace.WriteLine("No connection, can't get exercises");
                return response;
            }
            try
            {
                response = await client.DeleteAsync(exercisesUrl + "/" + exId);
            }
            catch (Exception e)
            {
                Trace.WriteLine("ERROR || " + e);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> IncrementExercisesCorrect()
        {
            HttpResponseMessage response = null;
            if (Connectivity.Current.NetworkAccess == NetworkAccess.None)
            {
                Trace.WriteLine("No connection, can't get exercises");
                return response;
            }
            try
            {
                response = await client.PostAsync(exercisesCorrectUrl, null);
            }
            catch (Exception e)
            {
                Trace.WriteLine("ERROR || " + e);
            }
            return response;
        }

        public static async Task<int> GetExercisesCorrect()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.None)
            {
                Trace.WriteLine("No connection, can't get exercises");
                return -1;
            }
            return await client.GetFromJsonAsync<int>(exercisesCorrectUrl);
        }
    }
}
