using System;
using System.Collections.Specialized;

namespace HW00
{
    internal class Program
    {
        public static void TestLimit()
        {
            Course testCourse = new Course("Test Course", "TEST01", new string[] { "Test Professor" }, new TimeFrame(8, 10, "Monday"), "A101", 1, 20);
            SeminarGroup testSeminar = new SeminarGroup(new string[] { "Test Professor" }, new TimeFrame(10, 12, "Monday"), "A102", 1, "Test Course", "TEST01", 1);
            testCourse.AddSeminarGroup(testSeminar);
            testCourse.AddSeminarGroup(testSeminar);  // should print msg about seminar group limit
            testSeminar.AddStudent("123456");
            testSeminar.AddStudent("234567");  // should print msg about seminar group limit
            Course testCourse2 = new Course("Test Course 2", "TEST02", new string[] { "Test Professor" }, new TimeFrame(12, 14, "Monday"), "A103", 0, 20);
            testCourse2.AddSeminarGroup(testSeminar);  // should print msg about not being able to add seminar group to course with 0 seminar group limit
        }

        public static void FillTimetable(Timetable tt)
        {
            Course cou1 = new Course("Programming in Java", "PB162", new string[] { "Doc. Oslejšek" }, new TimeFrame(8, 10, "Tuesday"), "A318", 10, 80);
            SeminarGroup sem1 = new SeminarGroup(new string[] { "Doc. Oslejšek, PhD." }, new TimeFrame(8, 10, "Monday"), "A219", 20, "Programming in Java", "PB162", 1);
            cou1.AddSeminarGroup(sem1);
            Course cou2 = new Course("Programming in C#", "PB178", new string[] { "RNDr. Macák, PhD." }, new TimeFrame(14, 16, "Thursday"), "A217", 10, 80);
            SeminarGroup sem2 = new SeminarGroup(new string[] { "T. Cvejn", "E. Hatalčíková" }, new TimeFrame(10, 12, "Tuesday"), "C119", 30, "Programming in C#", "PB178", 10);
            cou2.AddSeminarGroup(sem2);

            cou1.AddStudent("446348");
            sem1.AddStudent("446348");
            cou2.AddStudent("446348");
            sem2.AddStudent("446348");

            tt.tuesday[1] = cou1;
            tt.tuesday[2] = cou1;
            tt.monday[1] = sem1;
            tt.monday[2] = sem1;
            tt.tuesday[3] = sem2;
            tt.tuesday[4] = sem2;
            tt.thursday[7] = cou2;
            tt.thursday[8] = cou2;
        }

        public static void Main()
        {
            Timetable tt = new Timetable();
            FillTimetable(tt);
            //TestLimit();
        }
    }
}