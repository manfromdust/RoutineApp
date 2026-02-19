using System;
using System.Collections.Generic;
using System.Text;

namespace HW00
{
    internal abstract class CourseGroup
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
                Console.WriteLine("Capacity of course/seminar group room is full. Student %d hasn't been added.", uco);
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
}
