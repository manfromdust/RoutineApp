using System;
using System.Collections.Generic;
using System.Text;

namespace HW00
{
    internal class SeminarGroup : CourseGroup
    {
        public int groupId;
        public SeminarGroup(string[] teachers, TimeFrame timeFrame, string room, int roomCap,
                string name, string id, int groupId) : base(teachers, timeFrame, room, roomCap, name, id)
        {
            this.groupId = groupId;
        }
    }
}
