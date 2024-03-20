using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

[Serializable]
class Group
{
    public decimal GroupId { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
    // no need to serialize this
    [field: NonSerialized]
    public int StudentsCount { get; set; }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
        StudentsCount = Students.Count;
    }
}

[Serializable]
class Student
{
    public decimal StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Group Group { get; set; }
}

class Task2
{
    static void Main()
    {
        var studentA = new Student { StudentId = 1, FirstName = "Alice", LastName = "Ake", Age = 10, Group = null };
        var studentB = new Student { StudentId = 2, FirstName = "Bob", LastName = "Boks", Age = 23, Group = null };
        var studentC = new Student { StudentId = 3, FirstName = "Bobbb", LastName = "Boks", Age = 23, Group = null };
        var students = new List<Student>() { studentA, studentB, studentC };
        var group = new Group { GroupId = 1, Name = "yahoo", Students = students, StudentsCount = students.Count };

        using (var stream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
            formatter.Serialize(stream, group);

            stream.Position = 0;
            var newGroup = (Group)formatter.Deserialize(stream);
            Console.WriteLine("Groups:");
            Console.WriteLine($"{newGroup.GroupId} {newGroup.Name}");
            Console.WriteLine("Students:");
            foreach (var student in newGroup.Students)
            {
                Console.WriteLine($"{student.StudentId} {student.FirstName} {student.LastName}  {student.Age}");
            }
            Console.WriteLine("Number of students:");
            Console.WriteLine(newGroup.StudentsCount);
        }
    }
}