using System;
using System.Collections.Generic;
using System.Text;

namespace HW00
{
    internal class Timetable
    {
        public CourseGroup[] monday = new CourseGroup[21 - 7];
        public CourseGroup[] tuesday = new CourseGroup[21 - 7];
        public CourseGroup[] wednesday = new CourseGroup[21 - 7];
        public CourseGroup[] thursday = new CourseGroup[21 - 7];
        public CourseGroup[] friday = new CourseGroup[21 - 7];
    }
}
