using System;
using System.Collections.Generic;
using System.Text;

namespace HW00
{
    internal abstract class CourseGroup
    {
        protected string[] students;
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
            students = new string[roomCap];
            this.name = name;
            this.id = id;
        }

        public void AddStudent(string uco)
        {
            if (studentsCount >= students.Length)
            {
                Console.WriteLine("Capacity of course/seminar group room is full. Student {0} hasn't been added.", uco);
                return;
            }
            students[studentsCount++] = uco;
        }

        public void RemoveStudent(string uco)
        {
            int idx = Array.IndexOf(students, uco, 0, studentsCount);
            if (idx != -1)
            {
                students[idx] = students[studentsCount - 1];
                studentsCount--;
            }
            else
            {
                Console.WriteLine("Student {0} is not enrolled in course/seminar group {1}.", uco, this.name);
            }
        }
    }
}
