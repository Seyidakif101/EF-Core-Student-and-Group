using Azure;
using EF.Core.Seyid.Contexts;
using EF.Core.Seyid.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
AppDbConext context = new AppDbConext();
EVVEL:
Console.WriteLine("1.Group yarat");
Console.WriteLine("2.Grouplari goster");
Console.WriteLine("3.Group sil");
Console.WriteLine("4.Group Update");
Console.WriteLine("5.Student yarat");
Console.WriteLine("6.Studentleri goster");
Console.WriteLine("7.Student sil");
Console.WriteLine("8.Student Update");
while (true)
{

    Console.Write("Secim: ");
    string? Input = Console.ReadLine();
    Console.Clear();
    switch (Input)
    {
        case "1":
        group:
            Console.Write("Group adi daxil et: ");
            string? groupname = Console.ReadLine();
            if (groupname == null)
            {
                Console.WriteLine("Zehmet olmasa duzgun deyer daxil et!");
                goto group;
            }
            Group group = new Group()
            {
                Name = groupname
            };
            context.Groups.Add(group);
            context.SaveChanges();
            Console.WriteLine("Group Ugurla yaradildi.");
            break;

        case "2":
            AllGroups(context);
            break;

        case "3":
            AllGroups(context);
            Console.Write("Group Id daxil et: ");
            string? SilIdGInput = Console.ReadLine();
            bool isSil = int.TryParse(SilIdGInput, out int SilGId);
            if (!isSil)
            {
                Console.WriteLine("Zehmet olmasa duzgun deyer daxil et!");
                break;
            }
            Group? deleteGroup = context.Groups.FirstOrDefault(g => g.Id == SilGId);
            if (deleteGroup is null)
            {
                Console.WriteLine("Silmek istediyiniz Group yoxdur");
                break;
            }
            context.Groups.Remove(deleteGroup);
            context.SaveChanges();
            Console.WriteLine("Group Ugurla silindi.");
            break;
        case "4":
            AllGroups(context);
        UpdateGroup:
            Console.Write("Group Id daxil et: ");
            string? updateIdGInput = Console.ReadLine();
            bool isUpdate = int.TryParse(updateIdGInput, out int updateGId);
            if (!isUpdate)
            {
                Console.WriteLine("Zehmet olmasa duzgun deyer daxil et!");
                goto UpdateGroup;
            }
            Group? updateGroup = context.Groups.FirstOrDefault(g => g.Id == updateGId);
            if (updateGroup is null)
            {
                Console.WriteLine("Deyismek istediyiniz Group yoxdur");
                goto UpdateGroup;
            }
        UpdateGroupName:
            Console.Write("Yeni Group adi daxil et: ");
            string? newGroupName = Console.ReadLine();
            if (newGroupName == null)
            {
                Console.WriteLine("Zehmet olmasa duzgun deyer daxil et!");
                goto UpdateGroupName;
            }
            updateGroup.Name = newGroupName;
            context.SaveChanges();
            Console.WriteLine("Group Ugurla deyisdirildi.");
            break;


        case "5":
            string? studenName = GettName();
            string? studenSurname = GettSurname();
            int age = GettAge();
            decimal grade = GettGrade();
            int groupId = GetGroupId(context);

            Student student = new Student()
            {
                Name = studenName,
                Surname = studenSurname,
                Age = age,
                Grade = grade,
                GroupId = groupId
            };
            context.Students.Add(student);
            context.SaveChanges();
            Console.WriteLine("Student yaradildi");
            break;

        case "6":
            AllStudent(context);
            break;

        case "7":

            AllStudent(context);
            Console.Write("Student Id: ");
            string? sidInput = Console.ReadLine();
            bool isSid = int.TryParse(sidInput, out int sid);
            if (!isSid)
            {
                Console.WriteLine("duzgun deyer daxil et");
                break;
            }
            Student? deleteStudent = context.Students.FirstOrDefault(s => s.Id == sid);
            if (deleteStudent is null)
            {
                Console.WriteLine("Silmek istediyiniz Student yoxdur");
                break;
            }
            context.Students.Remove(deleteStudent);
            context.SaveChanges();
            Console.WriteLine("Student silindi.");
            break;
        case "8":
            AllStudent(context);
            Console.Write("Student Id: ");
            string? UpdateidInput = Console.ReadLine();
            bool isUpdateSid = int.TryParse(UpdateidInput, out int Updatesid);
            if (!isUpdateSid)
            {
                Console.WriteLine("Zehmet olmasa duzgun deyer daxil et!");
                break;
            }
            Student? UpdateStudent = context.Students.FirstOrDefault(s => s.Id == Updatesid);
            if (UpdateStudent is null)
            {
                Console.WriteLine("Update elemek istediyiniz Student yoxdur");
                break;
            }
            int newAge = GettAge();
            decimal newGrade = GettGrade();
            string newStudentSurname = GettSurname();
            string newStudentName = GettName();
            int newGroupId = GetGroupId(context);
            context.SaveChanges();
            Console.WriteLine("Student Ugurla deyisdirildi.");
            break;
        default:
            break;
    }
    goto EVVEL;
}
static void AllGroups(AppDbConext context)
{
    var groups = context.Groups.ToList();
    groups.ForEach(g => Console.WriteLine(g));
}

static void AllStudent(AppDbConext context)
{
    var students = context.Students.Include(s => s.Group).ToList();
    students.ForEach(student => Console.WriteLine(student));
}

static string GettName()
{
name:
    Console.Write("Name daxil et: ");
    string? studenName = Console.ReadLine();
    if (studenName == null || studenName.Length < 3)
    {
        Console.WriteLine("Zehmet olmasa duzgun deyer daxil et!");
        goto name;
    }

    return studenName;
}

static string GettSurname()
{
surname:
    Console.Write("Surname daxil et: ");
    string? studenSurname = Console.ReadLine();
    if (studenSurname == null || studenSurname.Length < 3)
    {
        Console.WriteLine("Zehmet olmasa duzgun deyer daxil et!");
        goto surname;
    }

    return studenSurname;
}

static int GettAge()
{
age:
    Console.Write("Age daxil et: ");
    string? ageInput = Console.ReadLine();
    var resultage = int.TryParse(ageInput, out int age);
    if (!resultage || age < 0)
    {
        Console.WriteLine("Zehmet olmasa duzgun deyer daxil et!");
        goto age;
    }

    return age;
}

static decimal GettGrade()
{
grade:
    Console.Write("Grade: ");
    string? gradeInput = Console.ReadLine();
    var resultgrade = decimal.TryParse(gradeInput, out decimal grade);
    if (!resultgrade || grade < 0 || grade > 100)
    {
        Console.WriteLine("Zehmet olmasa duzgun deyer daxil et!");
        goto grade;
    }

    return grade;
}

static int GetGroupId(AppDbConext context)
{
    var groupsList = context.Groups.ToList();
    groupsList.ForEach(g => Console.WriteLine(g));
groupIdInput:
    Console.Write("Group Id: ");
    string? groupIdInput = Console.ReadLine();
    var resultgroupid = int.TryParse(groupIdInput, out int groupId);
    if (!resultgroupid || groupId < 0)
    {
        Console.WriteLine("duzgun deyer daxil et");
        goto groupIdInput;
    }
    bool isGroupExists = context.Groups.Any(g => g.Id == groupId);
    if (!isGroupExists)
    {
        Console.WriteLine("Bele bir group yoxdur");
        goto groupIdInput;
    }

    return groupId;
}