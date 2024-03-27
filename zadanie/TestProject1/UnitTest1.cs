using LegacyApp;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void AddUser_Should_Return_True()
    {
        //Arrange
        var firstName = "Tomasz";
        var lastName = "Kowalski";
        var email = "blablabla@l.a";
        var date = new DateTime(1985,10,20);
        
        //Act
        var userService = new UserService();
        var a = userService.AddUser(firstName, lastName, email, date, 2);
        
        //Assert
        Assert.True(a);

    }
    
    [Fact]
    public void AddUser_Should_Return_False_Because_CreditLimit()
    {
        //Arrange
        var firstName = "Tomasz";
        var lastName = "Kowalski";
        var email = "blablabla@l.a";
        var date = new DateTime(1985,10,20);
        
        //Act
        var userService = new UserService();
        var a = userService.AddUser(firstName, lastName, email, date, 1);
        
        //Assert
        Assert.False(a);

    }
    
    [Fact]
    public void AddUser_Should_Return_False_Because_Date_Today_And_Age_Is_Zero()
    {
        //Arrange
        var firstName = "Tomasz";
        var lastName = "Kowalski";
        var email = "blablabla@l.a";
        var date = DateTime.Today;
        
        //Act
        var userService = new UserService();
        var a = userService.AddUser(firstName, lastName, email, date, 2);
        
        //Assert
        Assert.False(a);

    }
    
    [Fact]
    public void AddUser_Should_Return_False_Because_Email_Incorrect()
    {
        //Arrange
        var firstName = "Tomasz";
        var lastName = "Kowalski";
        var email = "blablabla@l.a";
        var date = DateTime.Today;
        
        //Act
        var userService = new UserService();
        var a = userService.AddUser(firstName, lastName, email, date, 2);
        
        //Assert
        Assert.False(a);

    }
    [Fact]
    public void AddUser_Should_Return_False_Because_Firstname_Empty()
    {
        //Arrange
        var firstName = "";
        var lastName = "Kowalski";
        var email = "blablabla@l.a";
        var date = DateTime.Today;
        
        //Act
        var userService = new UserService();
        var a = userService.AddUser(firstName, lastName, email, date, 3);
        
        //Assert
        Assert.False(a);

    }
    
    // testować tylko add user i publiczne
}