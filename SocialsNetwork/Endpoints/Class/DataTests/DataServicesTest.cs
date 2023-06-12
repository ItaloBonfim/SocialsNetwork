using SocialsNetwork.Infra.Data.IServices;

namespace SocialsNetwork.Endpoints.Class.DataTests
{
    public class DataServicesTest
    {

        public static string Template => "/TESTER/SERVICES";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action(IDataBase query)
        {

            var data = await query.GetDataBase("a0bb4117-7762-492c-8137-d4452a487503");
            return Results.Ok();
        }
    }
}
