using System;
using System.Collections.Specialized;

public class TimeFrame
{
    public int from;
    public int to;
    public string day;

    public TimeFrame(int begin, int end, string day)
    {
        from = begin;
        to = end;
        this.day = day;
    }
}

public abstract class CourseGroup
{
    protected int[] students;
    protected int studentsCount = 0;
    public string[] teachers;
    public TimeFrame timeFrame;
    public string name;
    public string id;
    public string room;

    public CourseGroup(string[] teachers, TimeFrame timeFrame, string room,
                        int roomCap, string name, string id)
    {
        this.teachers = teachers;
        this.timeFrame = timeFrame;
        this.room = room;
        students = new int[roomCap];
        this.name = name;
        this.id = id;
    }

    public void AddStudent(int uco)
    {
        if (studentsCount >= students.Length)
        {
            Console.WriteLine("Capacity of seminar group room is full. Student %d cannot be added", uco);
            return;
        }
        students[studentsCount++] = uco;
    }

    public void RemoveStudent(string name)
    {
        int idx = Array.IndexOf(students, name, 0, studentsCount);
        if (idx != -1)
        {
            students[idx] = students[studentsCount - 1];
            studentsCount--;
        }
    }
}

public class SeminarGroup : CourseGroup
{
    public int groupId;
    public SeminarGroup(string[] teachers, TimeFrame timeFrame, string room, int roomCap,
            string name, string id, int groupId) : base(teachers, timeFrame, room, roomCap, name, id)
    {
        this.groupId = groupId;
    }
}

public class Course : CourseGroup
{
    public SeminarGroup[]? seminarGroup;
    private int seminarGroupCount = 0;

    public Course(string name, string id, string[] teachers, TimeFrame timeFrame,
                  string room, int semGroups, int roomCap)
                 : base(teachers, timeFrame, room, roomCap, name, id)
    {
        if (semGroups > 0)
        {
            seminarGroup = new SeminarGroup[semGroups];
        }
    }

    public void AddSeminarGroup(SeminarGroup semGroup)
    {
        if (seminarGroup == null)
        {
            Console.WriteLine("Course %s does not support seminar groups.", name);
            return;
        }

        if (seminarGroupCount >= seminarGroup.Length)
        {
            Console.WriteLine("Cannot add seminar group to course %s. Maximum number of seminar groups reached.", name);
            return;
        }

        seminarGroup[seminarGroupCount++] = semGroup;
    }
}

public class Timetable
{
    public CourseGroup[] monday = new Course[21 - 7];
    public CourseGroup[] tuesday = new Course[21 - 7];
    public CourseGroup[] wednesday = new Course[21 - 7];
    public CourseGroup[] thursday = new Course[21 - 7];
    public CourseGroup[] friday = new Course[21 - 7];
}

public class Program
{
    public void PrintTimetable(Timetable tt)
    {

    }

    public static void fillTimetable(Timetable tt)
    {
        Course cou1 = new Course("Programming in Java", "PB162", new string[] { "Doc. Oslejšek" }, new TimeFrame(8, 10, "Tuesday"), "A318", 10, 80);
        SeminarGroup sem1 = new SeminarGroup(new string[] { "Doc. Oslejšek" }, new TimeFrame(8, 10, "Monday"), "A219", 20, "Programming in Java", "PB162", 1);
        cou1.AddSeminarGroup(sem1);

        tt.tuesday[1] = cou1;
        tt.tuesday[2] = cou1;
        tt.monday[1] = sem1;
        tt.tuesday[1] = new Course("Physics", "PHYS101", new string[] { "Dr. Johnson" }, new TimeFrame(9, 0), "Room 102");
        tt.wednesday[2] = new Course("Chemistry", "CHEM101", new string[] { "Dr. Lee" }, new TimeFrame(10, 0), "Room 103");
        tt.thursday[3] = new Course("Biology", "BIO101", new string[] { "Dr. Brown" }, new TimeFrame(11, 0), "Room 104");
        tt.friday[4] = new Course("Computer Science", "CS101", new string[] { "Dr. Davis" }, new TimeFrame(12, 0), "Room 105");
    }

    public static void Main()
    {
        
    }
}