using System;
using System.Collections.Generic;
using System.Text;

namespace HW00
{
    internal class Course : CourseGroup
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
                Console.WriteLine("Course {0} does not support seminar groups.", name);
                return;
            }

            if (seminarGroupCount >= seminarGroup.Length)
            {
                Console.WriteLine("Cannot add seminar group to course {0}. Maximum number of seminar groups reached.", name);
                return;
            }

            seminarGroup[seminarGroupCount++] = semGroup;
        }
    }
}
