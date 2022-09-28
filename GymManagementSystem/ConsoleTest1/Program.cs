using GymManagementSystem.Domain;
using System.Text.Json;


var myClassArr = new List<MyClass>();
myClassArr.Add(new MyClass() { Id = 0, Name = "0", Group = "A"});
myClassArr.Add(new MyClass() { Id = 1, Name = "1", Group = "A" });
myClassArr.Add(new MyClass() { Id = 2, Name = "2", Group = "A" });
myClassArr.Add(new MyClass() { Id = 3, Name = "3", Group = "B" });
myClassArr.Add(new MyClass() { Id = 4, Name = "4", Group = "B" });
myClassArr.Add(new MyClass() { Id = 5, Name = "5", Group = "B" });

var myGroups = myClassArr.GroupBy(c => c.Group);

for (int i = 0; i < 0; i++)
{
    Console.WriteLine("0");
}

Console.ReadLine();



public class MyClass
{
    public int Id { get; set; }
    public string Group { get; set; }
    public string Name { get; set; }
}