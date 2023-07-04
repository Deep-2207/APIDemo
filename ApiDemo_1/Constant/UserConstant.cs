using ApiDemo_1.Model;

namespace ApiDemo_1.Constant
{
    public class UserConstant
    {
        public static List<UserModel> users = new List<UserModel>()
        {
            new UserModel() { UserName = "Jason_admin", Email = "Jason@gmail.com",Password = "1234@Jason",Role = "Admin", SurName = "Patel",GaveNumber = "1234" },
            new UserModel() { UserName = "elyse_seller", Email = "elyse@gmail.com",Password = "1234@elyse",Role = "Seller", SurName = "Patel",GaveNumber = "7890" },
        };
    }
}
