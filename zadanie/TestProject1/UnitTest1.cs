using LegacyApp;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void AddUser_Should_Return_True()
    {
        //Arrange
        string first_name = "Tomasz";
        string last_name = "Kowalski";
        string adress = "blablabla";
        string email = "blablabla@l.a";
        DateTime date = new DateTime(1985,10,20);
        
        //Act
        UserService userService = new UserService();
        bool a = userService.AddUser(first_name, last_name, email, date, 1);
        
        //Assert
        Assert.True(a);

    }
    
    [Fact]
    public void AddUser_Should_Return_False_Because_Date_Today_And_Age_Is_Zero()
    {
        //Arrange
        string first_name = "Tomasz";
        string last_name = "Kowalski";
        string adress = "blablabla";
        string email = "blablabla@l.a";
        DateTime date = DateTime.Today;
        
        //Act
        UserService userService = new UserService();
        bool a = userService.AddUser(first_name, last_name, email, date, 1);
        
        //Assert
        Assert.False(a);

    }
    
    [Fact]
    public void AddUser_Should_Return_False_Because_Email_Incorrect()
    {
        //Arrange
        string first_name = "Tomasz";
        string last_name = "Kowalski";
        string adress = "blablabla";
        string email = "blablablala";
        DateTime date = DateTime.Today;
        
        //Act
        UserService userService = new UserService();
        bool a = userService.AddUser(first_name, last_name, email, date, 2);
        
        //Assert
        Assert.False(a);

    }
    [Fact]
    public void AddUser_Should_Return_False_Because_Firstname_Empty()
    {
        //Arrange
        string first_name = "";
        string last_name = "Kowalski";
        string adress = "blablabla";
        string email = "blablabla@l.a";
        DateTime date = DateTime.Today;
        
        //Act
        UserService userService = new UserService();
        bool a = userService.AddUser(first_name, last_name, email, date, 3);
        
        //Assert
        Assert.False(a);

    }
    
    // testować tylko add user i publiczne
}